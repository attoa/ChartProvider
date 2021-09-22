using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ChartProviders
{
    public partial class ChartSetsWindow : Form
    {
        List<ChartProvider> _chartProviders = new List<ChartProvider>();    // Список связанных ChartProvider'ов

        Button _chartSetsWindowButton;      // Ссылка на кнопку открытия/закрытия окна, по умолчанию отсутствует

        static string autoText = "auto";    // Текст, отображаемый в textBox если значение оси опред-ся автоматически

        /// <summary>
        /// Конструктор для одного ChartProvider'а
        /// </summary>
        /// <param name="chartProvider">Связанный ChartProvider</param>
        public ChartSetsWindow(ChartProvider chartProvider)
        {
            InitializeComponent();

            _chartProviders.Add(chartProvider);
        }

        /// <summary>
        /// Конструктор для нескольких ChartProvider'ов
        /// </summary>
        /// <param name="chartProviders">Массив связанных ChartProvider'ов</param>
        public ChartSetsWindow(ChartProvider[] chartProviders)
        {
            InitializeComponent();

            _chartProviders.AddRange(chartProviders);
            this.Text = "Настройки графиков";
        }

        /// <summary>
        /// Установка ссылки на кнопку открытия/закрытия окна
        /// </summary>
        /// <param name="chartSetsWindowButton">Кнопка открытия/закрытия окна</param>
        public void SetChartSetsWindowButton(Button chartSetsWindowButton)
        {
            _chartSetsWindowButton = chartSetsWindowButton;
        }


        /// <summary>
        /// Действия при загрузке формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChartSetsWindow_Load(object sender, EventArgs e)
        {
            // Получение значений полей из 1-го связанного ChartProvider'а
            comboBoxChartType.SelectedItem = _chartProviders[0].ChartType;
            numericUpDownLineWidth.Value = _chartProviders[0].LineWidth;
            checkBoxSingleColor.Checked = _chartProviders[0].SingleColor;

            if (!Double.IsNaN(_chartProviders[0].Xmin)) textBoxXmin.Text = _chartProviders[0].Xmin.ToString();
            else textBoxXmin.Text = autoText;

            if (!Double.IsNaN(_chartProviders[0].Xmax)) textBoxXmax.Text = _chartProviders[0].Xmax.ToString();
            else textBoxXmax.Text = autoText;

            if (!Double.IsNaN(_chartProviders[0].Ymin)) textBoxYmin.Text = _chartProviders[0].Ymin.ToString();
            else textBoxYmin.Text = autoText;

            if (!Double.IsNaN(_chartProviders[0].Ymax)) textBoxYmax.Text = _chartProviders[0].Ymax.ToString();
            else textBoxYmax.Text = autoText;

            if (!Double.IsNaN(_chartProviders[0].Xint)) textBoxXint.Text = _chartProviders[0].Xint.ToString();
            else textBoxXint.Text = autoText;

            if (!Double.IsNaN(_chartProviders[0].Yint)) textBoxYint.Text = _chartProviders[0].Yint.ToString();
            else textBoxYint.Text = autoText;
        }

        /// <summary>
        /// Действия при нажатии кнопки 'OK'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Действия при закрытии формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChartSetsWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_chartSetsWindowButton != null)
                _chartSetsWindowButton.Enabled = true;
        }


        /// <summary>
        /// Действия при изменении поля 'Тип графика'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _chartProviders.ForEach(c => c.SetChartType(comboBoxChartType.SelectedItem.ToString()));
        }

        /// <summary>
        /// Действия при изменении поля 'Толщина линий графика'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericUpDownLineWidth_ValueChanged(object sender, EventArgs e)
        {
            _chartProviders.ForEach(c => c.SetChartLineWidth((int)numericUpDownLineWidth.Value));
        }

        /// <summary>
        /// Действия при изменении флага 'Одинаковый цвет линий графика'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxSingleColor_CheckedChanged(object sender, EventArgs e)
        {
            _chartProviders.ForEach(c => c.SetChartSingleLineColor(checkBoxSingleColor.Checked));
        }

        
        /// <summary>
        /// Действия при вводе в поле 'Минимум оси X'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxXmin_TextChanged(object sender, EventArgs e)
        {
            if (textBoxXmin.Text == "")
                _chartProviders.ForEach(c => c.SetAxisesAutoValues(xmin: true));

            else if (SetXmin())
                SetXmax();
        }
        
        /// <summary>
        /// Действия при вводе в поле 'Максимум оси X'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxXmax_TextChanged(object sender, EventArgs e)
        {
            if (textBoxXmax.Text == "")
                _chartProviders.ForEach(c => c.SetAxisesAutoValues(xmax: true));

            else if (SetXmax())
                SetXmin();
        }
        
        /// <summary>
        /// Действия при вводе в поле 'Минимум оси Y'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxYmin_TextChanged(object sender, EventArgs e)
        {
            if (textBoxYmin.Text == "")
                _chartProviders.ForEach(c => c.SetAxisesAutoValues(ymin: true));

            else if (SetYmin())
                SetYmax();
        }
        
        /// <summary>
        /// Действия при вводе в поле 'Максимум оси Y'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxYmax_TextChanged(object sender, EventArgs e)
        {
            if (textBoxYmax.Text == "")
                _chartProviders.ForEach(c => c.SetAxisesAutoValues(ymax: true));

            else if (SetYmax())
                SetYmin();
        }
        
        /// <summary>
        /// Действия при вводе в поле 'Интервал оси X'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxXint_TextChanged(object sender, EventArgs e)
        {
            if (textBoxXint.Text == "")
                _chartProviders.ForEach(c => c.SetAxisesAutoValues(xint: true));
            else
            {
                try
                {
                    double xint = Convert.ToDouble(textBoxXint.Text);
                    _chartProviders.ForEach(c => c.SetAxisesValues(xint: xint));
                }
                catch { }
            }
        }
        
        /// <summary>
        /// Действия при вводе в поле 'Интервал оси Y'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxYint_TextChanged(object sender, EventArgs e)
        {
            if (textBoxYint.Text == "")
                _chartProviders.ForEach(c => c.SetAxisesAutoValues(yint: true));
            else
            {
                try
                {
                    double yint = Convert.ToDouble(textBoxYint.Text);
                    _chartProviders.ForEach(c => c.SetAxisesValues(yint: yint));
                }
                catch { }
            }
        }


        /// <summary>
        /// Действия при нажатии клавиш в поле ввода значения оси
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Запрет на ввод всего, кроме цифр, BackSpace, минуса и запятой
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8 && e.KeyChar != 44 && e.KeyChar != 45)
                e.Handled = true;
        }

        /// <summary>
        /// Действия при входе в поле ввода значения оси
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_Enter(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text == autoText)
                ((TextBox)sender).Text = "";
        }

        /// <summary>
        /// Действия при уходе из поля ввода значения оси
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_Leave(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text == "")
                ((TextBox)sender).Text = autoText;
        }


        /// <summary>
        /// Установить значение xmin
        /// </summary>
        /// <returns>Удалось ли выполнить конвертацию в число</returns>
        private bool SetXmin()
        {
            try
            {
                double xmin = Convert.ToDouble(textBoxXmin.Text);
                _chartProviders.ForEach(c => c.SetAxisesValues(xmin: xmin));
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Установить значение xmax
        /// </summary>
        /// <returns>Удалось ли выполнить конвертацию в число</returns>
        private bool SetXmax()
        {
            try
            {
                double xmax = Convert.ToDouble(textBoxXmax.Text);
                _chartProviders.ForEach(c => c.SetAxisesValues(xmax: xmax));
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Установить значение ymin
        /// </summary>
        /// <returns>Удалось ли выполнить конвертацию в число</returns>
        private bool SetYmin()
        {
            try
            {
                double ymin = Convert.ToDouble(textBoxYmin.Text);
                _chartProviders.ForEach(c => c.SetAxisesValues(ymin: ymin));
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Установить значение ymax
        /// </summary>
        /// <returns>Удалось ли выполнить конвертацию в число</returns>
        private bool SetYmax()
        {
            try
            {
                double ymax = Convert.ToDouble(textBoxYmax.Text);
                _chartProviders.ForEach(c => c.SetAxisesValues(ymax: ymax));
                return true;
            }
            catch { return false; }
        }
    }
}