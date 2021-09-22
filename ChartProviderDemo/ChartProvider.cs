using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using System.IO;
using OfficeOpenXml;

namespace ChartProviders
{
    public class ChartProvider
    {
        protected Chart _chart;                     // Ссылка на связанный график
        protected TextBox _textBox;                 // Ссылка на textBox, по умолчанию отсутствует

        protected string _xName = "X",              // Имена измеряемых величин по осям
                         _yName = "Y";

        protected bool horLinesAreAdded = false;                // Были ли добавлены горизонтальные линии
        protected List<HorLine> horLines = new List<HorLine>(); // Список добавленных горизонтальных линий

        protected int k = 0,                        // Счетчик номера замера
                      kmax = 9;                     // Макс. значение счетчика (макс. кол-во замеров)

        protected List<double>[] _xPoints,          // Массив точек замеров по осям
                                 _yPoints;          // (исп-ся List т.к. длины замеров м.б. различны)

        protected bool[] states;                    // Массив доступности замеров

        protected Point     zoomPoint1, zoomPoint2; // Точки для увеличения масштаба графика
        protected Rectangle zoomRect;               // Область увеличения графика
        protected bool      zoomIsAllowed = true;   // Разрешено ли увеличение графика мышью (false - если мышь
                                                    // нажата за пределами осей или захвачена гор. линия)

        protected Axis AX, AY;                      // Ссылки на оси графика (для сокращения кода)

        protected ChartSetsWindow setsWindow;       // Окно настроек графика, по умолчанию отсутствует

        protected double _xmin = Double.NaN,        // Значения осей графика, по умолчанию NaN
                         _xmax = Double.NaN,        // (NaN - значение по умолчанию у chart.NET.
                         _ymin = Double.NaN,        // В этом случае значения осей он определяет сам)
                         _ymax = Double.NaN,
                         _xint = Double.NaN,
                         _yint = Double.NaN;

        protected double _xmin_, _xmax_,            // Значения осей графика до увеличения мышью
                         _ymin_, _ymax_,            // _xint_ = 0 - флаг, указывающий, что увеличения не было
                         _xint_, _yint_;

        protected SeriesChartType _chartType = SeriesChartType.Line;    // Тип графика
        protected int _lineWidth = 1;                                   // Толщина линий графика
        protected bool _isSingleColor = false;                          // Одного ли цвета линии графика

        protected static Color[] colors = {                             // Массив цветов линий графика
            Color.FromArgb(255, 192, 0, 0),
            Color.DarkOrange,
            SystemColors.HotTrack,
            Color.FromArgb(255, 0, 192, 0),
            Color.Purple,
            Color.Peru,
            Color.BlueViolet,
            Color.DimGray,
            Color.Navy,
            Color.Magenta };


        #region Свойства
        /// <summary>
        /// Возвращает значение минимума оси X
        /// </summary>
        public double Xmin { get { return _xmin; } }
        /// <summary>
        /// Возвращает значение максимума оси X
        /// </summary>
        public double Xmax { get { return _xmax; } }
        /// <summary>
        /// Возвращает значение минимума оси Y
        /// </summary>
        public double Ymin { get { return _ymin; } }
        /// <summary>
        /// Возвращает значение максимума оси Y
        /// </summary>
        public double Ymax { get { return _ymax; } }
        /// <summary>
        /// Возвращает значение интервала оси X
        /// </summary>
        public double Xint { get { return _xint; } }
        /// <summary>
        /// Возвращает значение интервала оси Y
        /// </summary>
        public double Yint { get { return _yint; } }

        /// <summary>
        /// Возвращает значение типа графика
        /// </summary>
        public string ChartType { get { return _chartType.ToString(); } }
        /// <summary>
        /// Возвращает значение толщины линий графика
        /// </summary>
        public int    LineWidth { get { return _lineWidth; } }
        /// <summary>
        /// Возвращает true если линии графика одного цвета
        /// </summary>
        public bool   SingleColor { get { return _isSingleColor; } }
        #endregion


        #region Основные методы
        /// <summary>
        /// Конструктор класса ChartProvider
        /// </summary>
        /// <param name="chart">Связанный график</param>
        /// <param name="numberOfMeasurements">Количество отображаемых замеров</param>
        public ChartProvider(Chart chart, int numberOfMeasurements = 10)
        {
            if (numberOfMeasurements >= 1)
                kmax = numberOfMeasurements - 1;
            else
                throw new Exception("Количество отображаемых замеров не может быть меньше 1");

            _chart = chart;

            // Инициализация массивов
            _xPoints = new List<double>[kmax + 1];
            _yPoints = new List<double>[kmax + 1];
            states = new bool[kmax + 1];

            // Удаление существующих серий и подписей к ним
            _chart.Series.Clear();
            _chart.Legends.Clear();

            // Оси графика
            AX = _chart.ChartAreas[0].AxisX;
            AY = _chart.ChartAreas[0].AxisY;
            // Оси всегда отображаются (в т.ч. при пустом графике)
            AX.Enabled = AxisEnabled.True;
            AY.Enabled = AxisEnabled.True;
            // Установка цвета сеток осей
            AX.MajorGrid.LineColor = Color.Silver;
            AY.MajorGrid.LineColor = Color.Silver;

            for (int i = 0; i <= kmax; i++)
            {
                // Добавление серий
                Series serie = new Series()
                {
                    ChartArea = _chart.ChartAreas[0].Name,
                    ChartType = SeriesChartType.Line,
                    Color = colors[i % 10],
                    Legend = "Legend1",
                    Name = "Series" + (i + 1)
                };
                _chart.Series.Add(serie);

                // Заполнение массивов
                _xPoints[i] = new List<double>();
                _yPoints[i] = new List<double>();
                states[i] = true;
            }

            // Привязка функций, реализующих увеличение графика мышью, к событиям
            _chart.MouseDown += new MouseEventHandler(chart_MouseDown);
            _chart.MouseUp += new MouseEventHandler(chart_MouseUp);
            _chart.MouseMove += new MouseEventHandler(chart_MouseMove);
            _chart.Paint += new PaintEventHandler(chart_Paint);
        }

        /// <summary>
        /// Добавление textBox для вывода значений замеров
        /// </summary>
        /// <param name="textBox">Связанный textBox</param>
        /// <param name="xName">Имя измеряемой величины по оси X</param>
        /// <param name="yName">Имя измеряемой величины по оси Y</param>
        public void AddTextDisplaying(TextBox textBox, string xName = "X", string yName = "Y")
        {
            _textBox = textBox;
            _xName = xName;
            _yName = yName;

            // Настройка свойств textBox'а
            _textBox.ReadOnly = true;
            _textBox.BackColor = Color.White;
            _textBox.ScrollBars = ScrollBars.Vertical;
        }

        /// <summary>
        /// Добавление горизонтальной линии
        /// </summary>
        /// <param name="horLineInput">Связанное поле ввода</param>
        /// <param name="horLineColor">Цвет линии</param>
        public void AddHorizontalLine(NumericUpDown horLineInput, Color horLineColor)
        {
            horLines.Add(new HorLine(_chart, horLineInput, horLineColor));

            horLineInput.ValueChanged += new EventHandler(input_ValueChanged);

            if (!horLinesAreAdded)
            {
                _chart.AnnotationPositionChanged += new EventHandler(chart_AnnotationPositionChanged);
                _chart.AnnotationPositionChanging += new EventHandler<AnnotationPositionChangingEventArgs>(chart_AnnotationPositionChanging);
            }
            horLinesAreAdded = true;
        }

        /// <summary>
        /// Отображение замера
        /// </summary>
        /// <param name="xPoints">Массив X-координат точек</param>
        /// <param name="yPoints">Массив Y-координат точек</param>
        /// <param name="numberOfPoints">Количество отображаемых точек</param>
        public void Display(double[] xPoints, double[] yPoints, int? numberOfPoints = null)
        {
            if (!PrepareToDisplay()) return;

            if (numberOfPoints == 0 || xPoints.Length == 0 || yPoints.Length == 0)
            {
                MessageBox.Show("Получено 0 точек.", "Ошибка!");
                return;
            }

            // Уменьшение длин массивов координат до минимальной из величин (длина X-массива,
            // длина Y-массива, кол-во отображаемых точек) если эти величины различны
            if (numberOfPoints != xPoints.Length || numberOfPoints != yPoints.Length || 
                xPoints.Length != yPoints.Length)
            {
                if (numberOfPoints == null)
                    numberOfPoints = Math.Min(xPoints.Length, yPoints.Length);
                else
                    numberOfPoints = Math.Min(Math.Min(xPoints.Length, yPoints.Length), numberOfPoints.Value);

                Array.Resize(ref xPoints, numberOfPoints.Value);
                Array.Resize(ref yPoints, numberOfPoints.Value);
            }

            // Добавление замера
            _xPoints[k].AddRange(xPoints);
            _yPoints[k].AddRange(yPoints);

            // Отображение замера
            for (int i = 0; i < _xPoints[k].Count && i < _yPoints[k].Count; i++)
                _chart.Series[k].Points.AddXY(_xPoints[k][i], _yPoints[k][i]);

            // Отображение горизонтальных линий если были добавлены
            if (horLinesAreAdded)
                DisplayHorizontalLines();

            // Вывод значений замеров в textBox если был добавлен
            if (_textBox != null)
                _textBox.Text = GenerateMeasurementsText();

            if (k == kmax) k = 0;
            else k++;
        }
        
        /// <summary>
        /// Удаление всех замеров
        /// </summary>
        public void DeleteAll()
        {
            for (int num = 0; num <= kmax; num++)
            {
                _chart.Series[num].Points.Clear();
                _xPoints[num].Clear();
                _yPoints[num].Clear();
            }

            _chart.Annotations.Clear();

            if (_textBox != null) 
                _textBox.Clear();

            k = 0;
        }

        /// <summary>
        /// Удаление одного замера
        /// </summary>
        /// <param name="num">Номер замера</param>
        public void DeleteMeasurement(int num)
        {
            num--;
            if (num < 0 || num > kmax) return;

            _chart.Series[num].Points.Clear();
            _xPoints[num].Clear();
            _yPoints[num].Clear();

            // Вывод значений замеров в textBox если он был добавлен
            if (_textBox != null) 
                _textBox.Text = GenerateMeasurementsText();

            // Выход если есть заполненные замеры
            for (int i = 0; i <= kmax; i++)
                if (_xPoints[i].Count > 0) return;                    

            // Удаление гор. линий, очистка textBox и сброс счетчика если заполненных замеров больше нет
            _chart.Annotations.Clear();

            if (_textBox != null)
                _textBox.Clear();

            k = 0;
        }

        /// <summary>
        /// Установка доступности замера
        /// </summary>
        /// <param name="num">Номер замера</param>
        /// <param name="state">Доступен ли замер</param>
        public void SetMeasurementAvailability(int num, bool state)
        {
            num--;
            if (num < 0 || num > kmax) return;

            states[num] = state;
            _chart.Series[num].Enabled = states[num];
        }

        /// <summary>
        /// Установка доступности увеличения графика мышью
        /// </summary>
        /// <param name="state">Доступно ли увеличение графика мышью</param>
        public void SetScalingByMouseAvailability(bool state)
        {
            _chart.MouseDown -= new MouseEventHandler(chart_MouseDown);
            _chart.MouseUp -= new MouseEventHandler(chart_MouseUp);
            _chart.MouseMove -= new MouseEventHandler(chart_MouseMove);
            _chart.Paint -= new PaintEventHandler(chart_Paint);

            if (state)
            {
                _chart.MouseDown += new MouseEventHandler(chart_MouseDown);
                _chart.MouseUp += new MouseEventHandler(chart_MouseUp);
                _chart.MouseMove += new MouseEventHandler(chart_MouseMove);
                _chart.Paint += new PaintEventHandler(chart_Paint);
            }
        }

        /// <summary>
        /// Сохранение графика и/или значений замеров в файл
        /// </summary>
        public void SaveToFileAs()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Title = "Сохранить как...";
            saveFileDialog.Filter = "Документ Excel (*.xlsx)|*.xlsx;|" +
                                    "Изображение (*.png)|*.png;|" +
                                    "Изображение (*.bmp)|*.bmp;|" +
                                    "Изображение (*.jpg)|*.jpg;|" +
                                    "Текстовый документ (*.txt)|*.txt";
            saveFileDialog.FileName = DateTime.Now.ToString("yyyy-MM-dd_HH-mm");

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                switch (saveFileDialog.FilterIndex)
                {
                    case 1: SaveToExcelFile(saveFileDialog.FileName); break;
                    case 2: _chart.SaveImage(saveFileDialog.FileName, ChartImageFormat.Png); break;
                    case 3: _chart.SaveImage(saveFileDialog.FileName, ChartImageFormat.Bmp); break;
                    case 4: _chart.SaveImage(saveFileDialog.FileName, ChartImageFormat.Jpeg); break;
                    case 5:
                    {
                        StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName);
                        streamWriter.WriteLine(GenerateMeasurementsText());
                        streamWriter.Close(); break;
                    }
                }
            }
        }

        /// <summary>
        /// Загрузка значений замеров из Excel-файла
        /// </summary>
        public void LoadFromExcelFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Открыть файл замеров";
            openFileDialog.Filter = "Документ Excel (*.xlsx)|*.xlsx";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                MemoryStream stream;

                try
                {   // Загрузка файла в поток
                    stream = new MemoryStream(File.ReadAllBytes(openFileDialog.FileName));
                }
                catch
                {
                    MessageBox.Show("Не удается открыть файл \"" + openFileDialog.FileName + "\" т.к. он открыт в другом приложении.",
                                    "Ошибка!");
                    return;
                }

                ExcelPackage excel = new ExcelPackage(stream);

                try
                {
                    // Пробег по листам документа
                    foreach (ExcelWorksheet worksheet in excel.Workbook.Worksheets)
                    {
                        if (!PrepareToDisplay()) return;

                        // Пробег по строкам
                        for (int startRowNumber = worksheet.Dimension.Start.Row + 1; startRowNumber <= worksheet.Dimension.End.Row; startRowNumber++)
                        {
                            // Если ячейки не пустые
                            if (worksheet.Cells["B" + startRowNumber].Value != null && worksheet.Cells["C" + startRowNumber].Value != null)
                            {
                                _xPoints[k].Add(Double.Parse(worksheet.Cells["B" + startRowNumber].Value.ToString()));
                                _yPoints[k].Add(Double.Parse(worksheet.Cells["C" + startRowNumber].Value.ToString()));
                                _chart.Series[k].Points.AddXY(_xPoints[k].Last(), _yPoints[k].Last()); // Отображение замера
                            }
                            // Если пустые - достигнут конец замера, подготовка к загрузке следующего
                            else
                            {
                                if (k == kmax) k = 0;
                                else k++;
                                PrepareToDisplay();
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Не удается прочитать файл \"" + openFileDialog.FileName + "\" т.к. он некорректен.", "Ошибка!");
                    return;
                }

                // Отображение горизонтальных линий если были добавлены
                if (horLinesAreAdded)
                    DisplayHorizontalLines();

                // Вывод значений замеров в textBox если он был добавлен
                if (_textBox != null)
                    _textBox.Text = GenerateMeasurementsText();

                if (k == kmax) k = 0;
                else k++;
            }
        }
        #endregion


        #region Методы для окна настроек графика
        /// <summary>
        /// Добавление окна настроек графика
        /// </summary>
        public void AddChartSetsWindow()
        {
            setsWindow = new ChartSetsWindow(this);
        }

        /// <summary>
        /// Добавление окна настроек для нескольких графиков
        /// </summary>
        /// <param name="_chartProviders">Объекты класса СhartProvider через запятую</param>
        public void AddChartSetsWindow(params ChartProvider[] chartProviders)
        {
            setsWindow = new ChartSetsWindow(chartProviders);
        }

        /// <summary>
        /// Открытие окна настроек графика/графиков
        /// </summary>
        /// <param name="chartSetsWindowButton">Кнопка открытия/закрытия окна</param>
        public void ShowChartSetsWindow(Button chartSetsWindowButton = null)
        {
            if (setsWindow == null) return; // Если окно настроек не было добавлено

            if (chartSetsWindowButton != null)
            {
                chartSetsWindowButton.Enabled = false;
                setsWindow.SetChartSetsWindowButton(chartSetsWindowButton);
            }

            setsWindow.Show();
        }

        /// <summary>
        /// Установка значений осей графика
        /// </summary>
        /// <param name="xmin">Минимум оси X</param>
        /// <param name="xmax">Максимум оси X</param>
        /// <param name="ymin">Минимум оси Y</param>
        /// <param name="ymax">Максимум оси Y</param>
        /// <param name="xint">Интервал оси X</param>
        /// <param name="yint">Интервал оси Y</param>
        public void SetAxisesValues(double? xmin = null, double? xmax = null,
                                    double? ymin = null, double? ymax = null,
                                    double? xint = null, double? yint = null)
        {
            if (xmin < AX.Maximum || (xmin != null && Double.IsNaN(AX.Maximum)))
            {
                _xmin = xmin.Value;
                AX.Minimum = _xmin;
            }
            if (xmax > AX.Minimum || (xmax != null && Double.IsNaN(AX.Minimum)))
            {
                _xmax = xmax.Value;
                AX.Maximum = _xmax;
            }
            if (ymin < AY.Maximum || (ymin != null && Double.IsNaN(AY.Maximum)))
            {
                _ymin = ymin.Value;
                AY.Minimum = _ymin;
                DetermineHorLinesVisibility();
            }
            if (ymax > AY.Minimum || (ymax != null && Double.IsNaN(AY.Minimum)))
            {
                _ymax = ymax.Value;
                AY.Maximum = _ymax;
                DetermineHorLinesVisibility();
            }
            if (xint > 0 && !Double.IsNaN(AX.Minimum) && !Double.IsNaN(AX.Maximum))
            {
                _xint = xint.Value;
                AX.Interval = _xint;
            }
            if (yint > 0 && !Double.IsNaN(AY.Minimum) && !Double.IsNaN(AY.Maximum))
            {
                _yint = yint.Value;
                AY.Interval = _yint;
            }

            // Установить автоматические значения осей для тех, которые переданы как NaN
            SetAxisesAutoValues(Double.IsNaN(xmin ?? 0), Double.IsNaN(xmax ?? 0),
                                Double.IsNaN(ymin ?? 0), Double.IsNaN(ymax ?? 0),
                                Double.IsNaN(xint ?? 0), Double.IsNaN(yint ?? 0));
        }

        /// <summary>
        /// Установка автоматических значений осей графика
        /// </summary>
        /// <param name="xmin">Минимум оси X</param>
        /// <param name="xmax">Максимум оси X</param>
        /// <param name="ymin">Минимум оси Y</param>
        /// <param name="ymax">Максимум оси Y</param>
        /// <param name="xint">Интервал оси X</param>
        /// <param name="yint">Интервал оси Y</param>
        public void SetAxisesAutoValues(bool xmin = false, bool xmax = false,
                                        bool ymin = false, bool ymax = false,
                                        bool xint = false, bool yint = false)
        {
            if (xmin) { _xmin = Double.NaN; AX.Minimum = _xmin; }
            if (xmax) { _xmax = Double.NaN; AX.Maximum = _xmax; }
            if (xmin || xmax)
            {
                _chart.ChartAreas[0].RecalculateAxesScale();

                if (Double.IsNaN(AX.Minimum) || Double.IsNaN(AX.Maximum))
                    xint = true;
            }

            if (ymin) { _ymin = Double.NaN; AY.Minimum = _ymin; }
            if (ymax) { _ymax = Double.NaN; AY.Maximum = _ymax; }
            if (ymin || ymax)
            {
                _chart.ChartAreas[0].RecalculateAxesScale(); // Пересчет установленного NaN в конкретное число

                if (Double.IsNaN(AY.Minimum) || Double.IsNaN(AY.Maximum)) // Если пересчет дал NaN (график пуст)
                    yint = true;

                DetermineHorLinesVisibility();
            }

            if (xint) { _xint = Double.NaN; AX.Interval = _xint; }
            if (yint) { _yint = Double.NaN; AY.Interval = _yint; }

            /* Если одно из значений Minimum или Maximum оси у объекта Chart равно NaN (такое бывает при 
            пустом графике), то интервал этой оси также устан-ся равным NaN (чтобы не "сломать" Chart) */
        }

        /// <summary>
        /// Установка типа графика
        /// </summary>
        /// <param name="chartType">Тип графика</param>
        public void SetChartType(string chartType)
        {
            switch (chartType)
            {
                case "Line": _chartType = SeriesChartType.Line; break;
                case "Spline": _chartType = SeriesChartType.Spline; break;
                case "StepLine": _chartType = SeriesChartType.StepLine; break;
                case "Column": _chartType = SeriesChartType.Column; break;
                case "Candlestick": _chartType = SeriesChartType.Candlestick; break;
                case "Point": _chartType = SeriesChartType.Point; break;
                default: return;
            }

            for (int i = 0; i <= kmax; i++)
                _chart.Series[i].ChartType = _chartType;
        }

        /// <summary>
        /// Установка толщины линий графика
        /// </summary>
        /// <param name="lineWidth">Значение толщины линий</param>
        public void SetChartLineWidth(int lineWidth)
        {
            if (lineWidth < 1) return;

            _lineWidth = lineWidth;

            for (int i = 0; i <= kmax; i++)
                _chart.Series[i].BorderWidth = _lineWidth;
        }

        /// <summary>
        /// Установка одинакового цвета линий графика
        /// </summary>
        /// <param name="singleColor">Одинакового ли цвета линии графика</param>
        public void SetChartSingleLineColor(bool isSingleColor)
        {
            _isSingleColor = isSingleColor;

            if (_isSingleColor)
                for (int i = 0; i <= kmax; i++)
                    _chart.Series[i].Color = colors[0];
            else
                for (int i = 0; i <= kmax; i++)
                    _chart.Series[i].Color = colors[i % 10];
        }
        #endregion


        #region Закрытые методы
        /// <summary>
        /// Проверка доступности замера и удаление предыдущего k-того замера перед отображением
        /// </summary>
        /// <returns>Наличие доступных замеров</returns>
        protected bool PrepareToDisplay()
        {
            if (!states[k])  // Если текущий замер не доступен
            {
                for (k++; k <= kmax; k++)   // Поиск доступных замеров до конца
                    if (states[k]) break;
                
                if (k == kmax + 1)          // Если в конце доступный замер не найден
                {
                    for (k = 0; k < kmax; k++)      // Поиск с начала
                        if (states[k]) break;
                    
                    if (k == kmax && !states[k])    // Если в конце опять не найден
                    {
                        _chart.Annotations.Clear();
                        MessageBox.Show("Ни один замер не отмечен.", "Ошибка!");
                        return false;
                    }
                }
            }
            // Удаление предыдущего k-того замера
            _chart.Series[k].Points.Clear();
            _xPoints[k].Clear();
            _yPoints[k].Clear();
            return true;
        }

        /// <summary>
        /// Получение текста замеров
        /// </summary>
        /// <returns>Текст замеров</returns>
        protected string GenerateMeasurementsText()
        {
            string text = _xName + "\t" + _yName;

            for (int i = 0; i < _xPoints.Length && i < _yPoints.Length; i++)
            {
                if (_xPoints[i].Count > 0 && _yPoints[i].Count > 0) // Если замер не пустой
                {
                    text += "\r\n\r\nЗамер " + (i + 1) + ":";

                    for (int j = 0; j < _xPoints[i].Count && j < _yPoints[i].Count; j++)
                        text += "\r\n" + _xPoints[i][j] + "\t" + _yPoints[i][j];
                }
            }
            return text;
        }

        /// <summary>
        /// Сохранение графика и значений замеров в Excel-файл
        /// </summary>
        /// <param name="fileName">Полное имя файла</param>
        protected void SaveToExcelFile(string fileName)
        {
            ExcelPackage excel = new ExcelPackage();
            ExcelWorksheet worksheet = excel.Workbook.Worksheets.Add("Лист1");

            string[,] ranges = new string[kmax + 1, 2]; // Диапазоны ячеек замеров (исп-ся для графика)

            worksheet.Cells["B1"].Value = _xName;
            worksheet.Cells["C1"].Value = _yName;

            int startRowNumber = 2;
            for (int i = 0; i < _xPoints.Length; i++)
            {
                if (_xPoints[i].Count > 0 && _yPoints[i].Count > 0) // Если замер не пустой
                {
                    worksheet.Cells["A" + startRowNumber].Value = "Замер " + (i + 1);

                    // Заполнение ячеек значениями замера
                    for (int j = 0; j < _xPoints[i].Count; j++) 
                    {
                        worksheet.Cells["B" + (startRowNumber + j)].Value = _xPoints[i][j];
                        worksheet.Cells["C" + (startRowNumber + j)].Value = _yPoints[i][j];
                    }

                    // Диапазон заполненных ячеек i-того замера (точки по X и по Y)
                    ranges[i, 0] = "B" + startRowNumber + ":B" + (startRowNumber + _xPoints[i].Count - 1);
                    ranges[i, 1] = "C" + startRowNumber + ":C" + (startRowNumber + _xPoints[i].Count - 1);

                    startRowNumber += _xPoints[i].Count + 1;
                }
            }

            // Создание и настройка графика
            var excelChart = worksheet.Drawings.AddChart("Замеры", OfficeOpenXml.Drawing.Chart.eChartType.XYScatterLines);
            excelChart.SetPosition(4, 0, 4, 0);
            excelChart.SetSize(800, 600);
            excelChart.XAxis.MinValue = _xmin;
            excelChart.XAxis.MaxValue = _xmax;
            excelChart.XAxis.MinorUnit = _xint;
            excelChart.XAxis.MajorUnit = _xint;
            excelChart.YAxis.MinValue = _ymin;
            excelChart.YAxis.MaxValue = _ymax;
            excelChart.YAxis.MinorUnit = _yint;
            excelChart.YAxis.MajorUnit = _yint;

            // Заполнение графика
            for (int i = 0; i < ranges.GetLength(0); i++)
            {
                if (ranges[i, 0] != null && ranges[i, 1] != null)
                {
                    var serie = excelChart.Series.Add(worksheet.Cells[ranges[i, 1]], worksheet.Cells[ranges[i, 0]]);
                    serie.Header = "Замер " + (i + 1);
                }
            }

            FileStream fileStream = new FileStream(fileName, FileMode.Create);
            excel.SaveAs(fileStream);
            fileStream.Close();
        }

        /// <summary>
        /// Отображение горизонтальной линии уровня
        /// </summary>
        protected void DisplayHorizontalLines()
        {
            _chart.Annotations.Clear();

            for (int i = 0; i < horLines.Count; i++)
            {
                _chart.Annotations.Add(horLines[i].HA);
                _chart.Annotations.Add(horLines[i].RA);
            }
        }

        /// <summary>
        /// Определить видимость горизонтальных линий в зависимости от значений оси Y
        /// </summary>
        protected void DetermineHorLinesVisibility()
        {
            for (int i = 0; i < horLines.Count; i++)
            {
                if (horLines[i].HA.Y < AY.Minimum || horLines[i].HA.Y > AY.Maximum)
                {
                    horLines[i].RA.Visible = false;
                    horLines[i].HA.Visible = false;
                }
                else
                {
                    horLines[i].RA.Visible = true;
                    horLines[i].HA.Visible = true;
                }
            }
        }

        /// <summary>
        /// Действия при изменении значения в поле ввода, связанном с горизонтальной линией 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void input_ValueChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < horLines.Count; i++)
            {
                if (horLines[i].horLineInput == sender)
                {
                    // Установка Y-координаты горизонтальной линии
                    horLines[i].HA.Y = (int)horLines[i].horLineInput.Value;
                    horLines[i].RA.Y = horLines[i].HA.Y - horLines[i].RA.Height / 2;
                    horLines[i].RA.Text = horLines[i].HA.Y.ToString();

                    _chart.Update();
                    DetermineHorLinesVisibility();
                    break;
                }
            }
        }

        /// <summary>
        /// Действия после изменения позиции горизонтальной линии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chart_AnnotationPositionChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < horLines.Count; i++)
            {
                if (sender == horLines[i].HA)
                {
                    horLines[i].horLineInput.Value = (int)horLines[i].HA.Y;

                    horLines[i].RA.Text = Convert.ToString((int)horLines[i].HA.Y);

                    break;
                }
            }
        }

        /// <summary>
        /// Действия при изменении позиции горизонтальной линии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chart_AnnotationPositionChanging(object sender, AnnotationPositionChangingEventArgs e)
        {
            for (int i = 0; i < horLines.Count; i++)
            {
                if (sender == horLines[i].HA)
                {
                    zoomIsAllowed = false;

                    // Если мышь вышла за границы оси графика
                    if (e.NewLocationY < AY.Minimum) { e.NewLocationY = AY.Minimum; }
                    if (e.NewLocationY > AY.Maximum) { e.NewLocationY = AY.Maximum; }

                    // Перемещение подписи вместе с линией
                    horLines[i].RA.Y = horLines[i].HA.Y - horLines[i].RA.Height / 2;
                    // Отображение в подписи текущей Y-позиции линии
                    horLines[i].RA.Text = ((int)e.NewLocationY).ToString();

                    _chart.Update();
                    break;
                }
            }
        }

        /// <summary>
        /// Действия при нажатии кнопки мыши на графике
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chart_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && zoomIsAllowed)
            {
                // Находится ли точка клика в пределах осей графика
                if (AX.PixelPositionToValue(e.X) > AX.Minimum && AX.PixelPositionToValue(e.X) < AX.Maximum &&
                    AY.PixelPositionToValue(e.Y) > AY.Minimum && AY.PixelPositionToValue(e.Y) < AY.Maximum)
                {
                    // Получение координат 1й точки увеличения масштаба графика
                    zoomPoint1.X = e.X;
                    zoomPoint1.Y = e.Y;

                    // Запомнить значения осей если график еще не увеличен мышью
                    if (_xint_ == 0)
                    {
                        _xmin_ = _xmin; _xmax_ = _xmax;
                        _ymin_ = _ymin; _ymax_ = _ymax;
                        _xint_ = _xint; _yint_ = _yint;
                    }
                }
                else
                    zoomIsAllowed = false;
            }
        }

        /// <summary>
        /// Действия при отпускании кнопки мыши на графике
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chart_MouseUp(object sender, MouseEventArgs e)
        {
            if (zoomIsAllowed)
            {
                if (e.Button == MouseButtons.Left && zoomPoint1.X != e.X && zoomPoint1.Y != e.Y)
                {
                    // Получение координат 1й точки увеличения масштаба графика
                    zoomPoint1.X = (int)(AX.PixelPositionToValue(zoomRect.X));
                    zoomPoint1.Y = (int)(AY.PixelPositionToValue(zoomRect.Y));
                    // Получение координат 2й точки увеличения масштаба графика
                    zoomPoint2.X = (int)(AX.PixelPositionToValue(zoomRect.X + zoomRect.Width));
                    zoomPoint2.Y = (int)(AY.PixelPositionToValue(zoomRect.Y + zoomRect.Height));

                    // Округление координат точек оси X
                    int delta = Math.Abs(zoomPoint1.X - zoomPoint2.X);   // Разница между min и max
                    if (delta > 20)
                    {
                        int digitsCount = (int)Math.Round(Math.Log10(delta) + 0.5);             // Кол-во цифр в числе delta
                        int powed10 = delta < 1000 ? 10 : (int)Math.Pow(10, digitsCount - 2);   // Множитель для округления
             
                        zoomPoint1.X = (int)Math.Round(zoomPoint1.X / (double)powed10) * powed10;
                        zoomPoint2.X = (int)Math.Round(zoomPoint2.X / (double)powed10) * powed10;
                    }
                    // Округление координат точек оси Y
                    delta = Math.Abs(zoomPoint1.Y - zoomPoint2.Y);
                    if (delta > 20)
                    {
                        int digitsCount = (int)Math.Round(Math.Log10(delta) + 0.5);
                        int powed10 = delta < 1000 ? 10 : (int)Math.Pow(10, digitsCount - 2);

                        zoomPoint1.Y = (int)Math.Round(zoomPoint1.Y / (double)powed10) * powed10;
                        zoomPoint2.Y = (int)Math.Round(zoomPoint2.Y / (double)powed10) * powed10;
                    }
                    /* Округление происходит при delta > 20, т.к. в этом случае chart устанавливает дробные деления оси.
                    Если delta < 1000, то значения округляются до 2-го порядка от разницы, иначе - до 3-го. */

                    // Перерисовка нулевым прямоугольником
                    zoomRect = new Rectangle(0, 0, 0, 0);
                    _chart.Invalidate();

                    // Установка значений осей
                    SetAxisesValues(zoomPoint1.X, zoomPoint2.X, zoomPoint2.Y, zoomPoint1.Y);
                    SetAxisesAutoValues(xint: true, yint: true);
                }

                // Если был клик правой кнопкой мыши - сбросить масштаб графика к значениям до увеличения
                else if (e.Button == MouseButtons.Right && _xint_ != 0)
                {
                    SetAxisesValues(_xmin_, _xmax_, _ymin_, _ymax_, _xint_, _yint_);
                    _xint_ = 0; // флаг
                }
            }
            else
                zoomIsAllowed = true;
        }

        /// <summary>
        /// Действия при перемещении мыши на графике
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chart_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && zoomIsAllowed)
            {
                // Находится ли 2я точка в пределах осей графика
                try // try т.к. PixelPositionToValue() генерирует исключение, когда мышь покидает объект chart 
                {
                    if (AX.PixelPositionToValue(e.X) > AX.Minimum && AX.PixelPositionToValue(e.X) < AX.Maximum)
                        zoomPoint2.X = e.X;
                }
                catch { }
                try
                { 
                    if (AY.PixelPositionToValue(e.Y) > AY.Minimum && AY.PixelPositionToValue(e.Y) < AY.Maximum)
                        zoomPoint2.Y = e.Y;
                }
                catch { }

                // Получение области увеличения графика
                zoomRect = new Rectangle(
                    Math.Min(zoomPoint1.X, zoomPoint2.X),
                    Math.Min(zoomPoint1.Y, zoomPoint2.Y),
                    Math.Abs(zoomPoint1.X - zoomPoint2.X),
                    Math.Abs(zoomPoint1.Y - zoomPoint2.Y));
                _chart.Invalidate();
            }
        }

        /// <summary>
        /// Действия при перерисовке графика
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chart_Paint(object sender, PaintEventArgs e)
        {
            SolidBrush brush = new SolidBrush(Color.FromArgb(30, Color.Gray));
            e.Graphics.FillRectangle(brush, zoomRect);
        }
        #endregion


        /// <summary>
        /// Класс, описывающий горизонтальную линию
        /// </summary>
        protected class HorLine
        {
            public HorizontalLineAnnotation HA; // Горизонтальная линия
            public RectangleAnnotation RA;      // Подпись горизонтальной линии
            public NumericUpDown horLineInput;  // Ссылка на поле ввода, связанное с гориз. линией

            static int number = 0;              // Кол-во созданных горизонтальных линий

            public HorLine(Chart chart, NumericUpDown horLineInput, Color horLineColor)
            {
                number++;
                this.horLineInput = horLineInput;

                // Горизонтальная линия
                HA = new HorizontalLineAnnotation()
                {
                    AxisY = chart.ChartAreas[0].AxisY,
                    AllowMoving = true,
                    IsInfinitive = true,
                    ClipToChartArea = chart.ChartAreas[0].Name,
                    Name = "Line" + number,
                    LineColor = horLineColor,
                    LineWidth = 2,
                    Y = (int)this.horLineInput.Value
                };

                // Подпись
                RA = new RectangleAnnotation()
                {
                    AxisY = chart.ChartAreas[0].AxisY,
                    IsSizeAlwaysRelative = false,
                    Width = 6,
                    Height = 0.1,
                    Name = "Rect" + number,
                    LineColor = Color.Empty,
                    BackColor = Color.Empty,
                    Y = (int)this.horLineInput.Value,
                    X = 0,
                    Text = ((int)this.horLineInput.Value).ToString(),
                    ForeColor = horLineColor,
                    Font = new Font("Arial", 8f)
                };
            }
        }
    }
}