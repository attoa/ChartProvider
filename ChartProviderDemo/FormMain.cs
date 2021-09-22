using System;
using System.Drawing;
using System.Windows.Forms;
using ChartProviders;

namespace ChartProviderDemo
{
    public partial class FormMain : Form
    {
        ChartProvider ChartMain;    // Объект управления главным графиком
        ChartProvider ChartExtra;   // Объект управления дополнительным графиком
        ChartProvider ChartSecond1; // Объект управления 1-м графиком на 2-й вкладке
        ChartProvider ChartSecond2; // Объект управления 2-м графиком на 2-й вкладке

        // Исходные значения оси Y для графиков на 2-й вкладке
        const int yminInitSecond = 0,
                  yintInitSecond = 1,
                  ymaxInitSecond = 20;

        // Размеры окна на 1-й и 2-й вкладках
        int formH1, formH2;

        // Конструктор главной формы
        public FormMain()
        {
            InitializeComponent();

            // Главный график
            ChartMain = new ChartProvider(chart1);  // Инициализация объекта
            ChartMain.AddTextDisplaying(textBox1, "Время", "Ток");
            ChartMain.AddHorizontalLine(numericMain1, Color.Red);
            ChartMain.AddHorizontalLine(numericMain2, Color.Blue);

            // Дополнительный график
            ChartExtra = new ChartProvider(chart2, 5);

            // Графики на 2-й вкладке
            ChartSecond1 = new ChartProvider(chart3, 1);
            ChartSecond1.AddHorizontalLine(numericSecond1, Color.Green);
            ChartSecond1.SetAxisesValues(xmin: 0, xmax: 100, xint: 10);
            ChartSecond1.SetScalingByMouseAvailability(false);
            ChartSecond2 = new ChartProvider(chart4, 1);
            ChartSecond2.AddHorizontalLine(numericSecond1, Color.Green);
            ChartSecond2.SetAxisesValues(xmin: 0);
            ChartSecond2.SetScalingByMouseAvailability(false);

            formH1 = Height;
            formH2 = 425;
        }

        #region Главный график

        // Кнопка "Сгенерировать замер"
        private void btnMainGetData_Click(object sender, EventArgs e)
        {
            double[] x = new double[] { 0, 0, 0, 0, 0, 0, 8, 142, 150, 156,
                189, 191, 198, 199, 204, 204, 205, 211, 209, 211,
                216, 216, 220, 221, 224, 227, 229, 235, 235, 242,
                246, 248, 253, 258, 263, 267, 267, 272, 280, 283,
                288, 299, 310, 325, 0, 0, 0, 0, 0, 0 };     // Массив X-координат точек (реальный замер)
            double[] y = new double[50];                    // Массив Y-координат точек

            Random rnd = new Random();
            double shift = rnd.Next(50);

            for (int i = 0; i < y.Length; i++)
            {
                x[i] += shift;
                y[i] = i * 10;
            }

            ChartMain.Display(x, y);

            numericMain1.Value = (decimal)shift * 5;
            numericMain2.Value = 50;
        }

        // Кнопка "Настройка графика"
        private void btnMainSets_Click(object sender, EventArgs e)
        {
            ChartMain.AddChartSetsWindow();
            ChartMain.ShowChartSetsWindow(btnMainSets);
        }
        // Кнопка "Загрузить замеры из файла"
        private void btnMainLoad_Click(object sender, EventArgs e)
        {
            ChartMain.LoadFromExcelFile();
        }
        // Кнопка "Сохранить замеры как"
        private void btnMainSave_Click(object sender, EventArgs e)
        {
            ChartMain.SaveToFileAs();
        }
        // Кнопка "Удалить все"
        private void btnMainDelAll_Click(object sender, EventArgs e)
        {
            ChartMain.DeleteAll();
        }

        // Кнопка "Удалить замер 1"
        private void btnMainDelS1_Click(object sender, EventArgs e)
        {
            ChartMain.DeleteMeasurement(1);
        }
        // Кнопка "Удалить замер 2"
        private void btnMainDelS2_Click(object sender, EventArgs e)
        {
            ChartMain.DeleteMeasurement(2);
        }
        // Кнопка "Удалить замер 3"
        private void btnMainDelS3_Click(object sender, EventArgs e)
        {
            ChartMain.DeleteMeasurement(3);
        }
        // Кнопка "Удалить замер 4"
        private void btnMainDelS4_Click(object sender, EventArgs e)
        {
            ChartMain.DeleteMeasurement(4);
        }
        // Кнопка "Удалить замер 5"
        private void btnMainDelS5_Click(object sender, EventArgs e)
        {
            ChartMain.DeleteMeasurement(5);
        }
        // Кнопка "Удалить замер 6"
        private void btnMainDelS6_Click(object sender, EventArgs e)
        {
            ChartMain.DeleteMeasurement(6);
        }
        // Кнопка "Удалить замер 7"
        private void btnMainDelS7_Click(object sender, EventArgs e)
        {
            ChartMain.DeleteMeasurement(7);
        }
        // Кнопка "Удалить замер 8"
        private void btnMainDelS8_Click(object sender, EventArgs e)
        {
            ChartMain.DeleteMeasurement(8);
        }
        // Кнопка "Удалить замер 9"
        private void btnMainDelS9_Click(object sender, EventArgs e)
        {
            ChartMain.DeleteMeasurement(9);
        }
        // Кнопка "Удалить замер 10"
        private void btnMainDelS10_Click(object sender, EventArgs e)
        {
            ChartMain.DeleteMeasurement(10);
        }

        // Установка доступности замера 1
        private void checkBoxMainS1_CheckedChanged(object sender, EventArgs e)
        {
            ChartMain.SetMeasurementAvailability(1, checkBoxMainS1.Checked);
        }
        // Установка доступности замера 2
        private void checkBoxMainS2_CheckedChanged(object sender, EventArgs e)
        {
            ChartMain.SetMeasurementAvailability(2, checkBoxMainS2.Checked);
        }
        // Установка доступности замера 3
        private void checkBoxMainS3_CheckedChanged(object sender, EventArgs e)
        {
            ChartMain.SetMeasurementAvailability(3, checkBoxMainS3.Checked);
        }
        // Установка доступности замера 4
        private void checkBoxMainS4_CheckedChanged(object sender, EventArgs e)
        {
            ChartMain.SetMeasurementAvailability(4, checkBoxMainS4.Checked);
        }
        // Установка доступности замера 5
        private void checkBoxMainS5_CheckedChanged(object sender, EventArgs e)
        {
            ChartMain.SetMeasurementAvailability(5, checkBoxMainS5.Checked);
        }
        // Установка доступности замера 6
        private void checkBoxMainS6_CheckedChanged(object sender, EventArgs e)
        {
            ChartMain.SetMeasurementAvailability(6, checkBoxMainS6.Checked);
        }
        // Установка доступности замера 7
        private void checkBoxMainS7_CheckedChanged(object sender, EventArgs e)
        {
            ChartMain.SetMeasurementAvailability(7, checkBoxMainS7.Checked);
        }
        // Установка доступности замера 8
        private void checkBoxMainS8_CheckedChanged(object sender, EventArgs e)
        {
            ChartMain.SetMeasurementAvailability(8, checkBoxMainS8.Checked);
        }
        // Установка доступности замера 9
        private void checkBoxMainS9_CheckedChanged(object sender, EventArgs e)
        {
            ChartMain.SetMeasurementAvailability(9, checkBoxMainS9.Checked);
        }
        // Установка доступности замера 10
        private void checkBoxMainS10_CheckedChanged(object sender, EventArgs e)
        {
            ChartMain.SetMeasurementAvailability(10, checkBoxMainS10.Checked);
        }

        #endregion

        #region Дополнительный график

        // Кнопка "Сгенерировать замер"
        private void btnExtraGetData_Click(object sender, EventArgs e)
        {
            double[] x = new double[100],
                     y = new double[100];

            Random rnd = new Random();
            Random rndShift = new Random();
            double shift = rndShift.Next(200);

            for (int i = 0; i < x.Length; i++)
            {
                x[i] = i*10;
                y[i] = i*2 + rnd.Next(100) + shift;
            }

            ChartExtra.Display(x, y);
        }

        // Кнопка "Настройка графика"
        private void btnExtraSets_Click(object sender, EventArgs e)
        {
            ChartExtra.AddChartSetsWindow();
            ChartExtra.ShowChartSetsWindow(btnExtraSets);
        }
        // Кнопка "Загрузить замеры из файла"
        private void btnExtraLoad_Click(object sender, EventArgs e)
        {
            ChartExtra.LoadFromExcelFile();
        }
        // Кнопка "Сохранить замеры как"
        private void btnExtraSave_Click(object sender, EventArgs e)
        {
            ChartExtra.SaveToFileAs();
        }
        // Кнопка "Удалить все"
        private void btnExtraDelAll_Click(object sender, EventArgs e)
        {
            ChartExtra.DeleteAll();
        }

        // Кнопка "Удалить замер 1"
        private void btnExtraDelS1_Click(object sender, EventArgs e)
        {
            ChartExtra.DeleteMeasurement(1);
        }
        // Кнопка "Удалить замер 2"
        private void btnExtraDelS2_Click(object sender, EventArgs e)
        {
            ChartExtra.DeleteMeasurement(2);
        }
        // Кнопка "Удалить замер 3"
        private void btnExtraDelS3_Click(object sender, EventArgs e)
        {
            ChartExtra.DeleteMeasurement(3);
        }
        // Кнопка "Удалить замер 4"
        private void btnExtraDelS4_Click(object sender, EventArgs e)
        {
            ChartExtra.DeleteMeasurement(4);
        }
        // Кнопка "Удалить замер 5"
        private void btnExtraDelS5_Click(object sender, EventArgs e)
        {
            ChartExtra.DeleteMeasurement(5);
        }

        // Установка доступности замера 1
        private void checkBoxExtraS1_CheckedChanged(object sender, EventArgs e)
        {
            ChartExtra.SetMeasurementAvailability(1, checkBoxExtraS1.Checked);
        }
        // Установка доступности замера 2
        private void checkBoxExtraS2_CheckedChanged(object sender, EventArgs e)
        {
            ChartExtra.SetMeasurementAvailability(2, checkBoxExtraS2.Checked);
        }
        // Установка доступности замера 3
        private void checkBoxExtraS3_CheckedChanged(object sender, EventArgs e)
        {
            ChartExtra.SetMeasurementAvailability(3, checkBoxExtraS3.Checked);
        }
        // Установка доступности замера 4
        private void checkBoxExtraS4_CheckedChanged(object sender, EventArgs e)
        {
            ChartExtra.SetMeasurementAvailability(4, checkBoxExtraS4.Checked);
        }
        // Установка доступности замера 5
        private void checkBoxExtraS5_CheckedChanged(object sender, EventArgs e)
        {
            ChartExtra.SetMeasurementAvailability(5, checkBoxExtraS5.Checked);
        }

        #endregion

        #region Графики на 2-й вкладке
        
        // Кнопка "Сгенерировать замер"
        private void btnSecondGetData_Click(object sender, EventArgs e)
        {
            double[] x = new double[101],
                     y = new double[101];

            const int minValue = yminInitSecond,
                      maxValue = ymaxInitSecond + 1;
            double[] ycount = new double[maxValue - minValue],
                     y2 = new double[maxValue - minValue];

            Random rnd = new Random();

            for (int i = 0; i < x.Length; i++)
            {
                x[i] = i;
                y[i] = rnd.Next(minValue, maxValue);

                ycount[(int)y[i] - minValue]++;
            }
            for (int i = 0; i < y2.Length; i++)
                y2[i] = i + minValue;

            ChartSecond1.Display(x, y);
            ChartSecond2.Display(ycount, y2);
        }

        // Кнопка "Удалить все"
        private void btnSecondDelAll_Click(object sender, EventArgs e)
        {
            ChartSecond1.DeleteAll();
            ChartSecond2.DeleteAll();
        }

        // Минимум
        private void numericSecondMin_ValueChanged(object sender, EventArgs e)
        {
            ChartSecond1.SetAxisesValues(ymin: (int)numericSecondMin.Value);
            ChartSecond2.SetAxisesValues(ymin: (int)numericSecondMin.Value);
        }        
        // Шаг
        private void numericSecondInt_ValueChanged(object sender, EventArgs e)
        {
            ChartSecond1.SetAxisesValues(yint: (int)numericSecondInt.Value);
            ChartSecond2.SetAxisesValues(yint: (int)numericSecondInt.Value);
        }
        // Максимум
        private void numericSecondMax_ValueChanged(object sender, EventArgs e)
        {
            ChartSecond1.SetAxisesValues(ymax: (int)numericSecondMax.Value);
            ChartSecond2.SetAxisesValues(ymax: (int)numericSecondMax.Value);
        }

        // Кнопка "Сброс"
        private void btnSecondReset_Click(object sender, EventArgs e)
        {
            numericSecondMin.Value = yminInitSecond;
            numericSecondInt.Value = yintInitSecond;
            numericSecondMax.Value = ymaxInitSecond;
        }

        // Тип графика "Линия"
        private void radioBtnSecondLine_CheckedChanged(object sender, EventArgs e)
        {
            ChartSecond1.SetChartType("Line");
        }
        // Тип графика "Точки"
        private void radioBtnSecondPoint_CheckedChanged(object sender, EventArgs e)
        {
            ChartSecond1.SetChartType("Point");
        }

        #endregion

        // Открытие 1-й вкладки
        private void tabPage1_Enter(object sender, EventArgs e)
        {
            Height = formH1;
            tabControl1.Height = Height - 42;
        }
        // Открытие 2-й вкладки
        private void tabPage2_Enter(object sender, EventArgs e)
        {
            Height = formH2;
            tabControl1.Height = Height - 42;
        }

        // Открытие формы
        private void FormMain_Load(object sender, EventArgs e)
        {
            // Ссылка на объект настроек (для сокращения кода)
            var prop = Properties.Settings.Default;

            // Загрузка настроек

            // Главный график
            ChartMain.SetAxisesValues(xmin: prop.xminMain, xmax: prop.xmaxMain,
                                      ymin: prop.yminMain, ymax: prop.ymaxMain, 
                                      xint: prop.xintMain, yint: prop.yintMain);
            ChartMain.SetChartType(prop.chartTypeMain);
            ChartMain.SetChartLineWidth(prop.lineWidthMain);
            ChartMain.SetChartSingleLineColor(prop.isSingleColorMain);

            // Дополнительный график
            ChartExtra.SetAxisesValues(xmin: prop.xminExtra, xmax: prop.xmaxExtra,
                                       ymin: prop.yminExtra, ymax: prop.ymaxExtra,
                                       xint: prop.xintExtra, yint: prop.yintExtra);
            ChartExtra.SetChartType(prop.chartTypeExtra);
            ChartExtra.SetChartLineWidth(prop.lineWidthExtra);
            ChartExtra.SetChartSingleLineColor(prop.isSingleColorExtra);

            // Графики на 2-й вкладке
            ChartSecond1.SetAxisesValues(ymin: prop.yminSecond, ymax: prop.ymaxSecond, yint: prop.yintSecond);
            ChartSecond1.SetChartType(prop.chartTypeSecond);
            ChartSecond2.SetAxisesValues(ymin: prop.yminSecond, ymax: prop.ymaxSecond, yint: prop.yintSecond);
            numericSecondMin.Value = (decimal)ChartSecond1.Ymin;
            numericSecondInt.Value = (decimal)ChartSecond1.Yint;
            numericSecondMax.Value = (decimal)ChartSecond1.Ymax;
            if (ChartSecond1.ChartType == "Line") radioBtnSecondLine.Checked = true;
            else radioBtnSecondPoint.Checked = true;
        }
        // Закрытие формы
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Ссылка на объект настроек (для сокращения кода)
            var prop = Properties.Settings.Default;

            // Сохранение настроек

            // Главный график
            prop.xminMain = ChartMain.Xmin;
            prop.xmaxMain = ChartMain.Xmax;
            prop.xintMain = ChartMain.Xint;
            prop.yminMain = ChartMain.Ymin;
            prop.ymaxMain = ChartMain.Ymax;
            prop.yintMain = ChartMain.Yint;
            prop.chartTypeMain = ChartMain.ChartType;
            prop.lineWidthMain = ChartMain.LineWidth;
            prop.isSingleColorMain = ChartMain.SingleColor;

            // Дополнительный график
            prop.xminExtra = ChartExtra.Xmin;
            prop.xmaxExtra = ChartExtra.Xmax;
            prop.xintExtra = ChartExtra.Xint;
            prop.yminExtra = ChartExtra.Ymin;
            prop.ymaxExtra = ChartExtra.Ymax;
            prop.yintExtra = ChartExtra.Yint;
            prop.chartTypeExtra = ChartExtra.ChartType;
            prop.lineWidthExtra = ChartExtra.LineWidth;
            prop.isSingleColorExtra = ChartExtra.SingleColor;

            // 1-й график на 2-й вкладке
            prop.yminSecond = ChartSecond1.Ymin;
            prop.ymaxSecond = ChartSecond1.Ymax;
            prop.yintSecond = ChartSecond1.Yint;
            prop.chartTypeSecond = ChartSecond1.ChartType;

            prop.Save();
        }
    }
}
