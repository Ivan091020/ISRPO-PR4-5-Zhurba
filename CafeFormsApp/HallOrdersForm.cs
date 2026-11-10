using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ZhurbaCafeForms;

public sealed class HallOrdersForm : Form
{
    private readonly ComboBox _cmbTable;
    private readonly ComboBox _cmbDish;
    private readonly NumericUpDown _numPortions;
    private readonly CheckBox _chkServiceDiscount;
    private readonly ListBox _lstOrderItems;
    private readonly Label _lblTotalResult;
    private readonly List<decimal> _lineTotals = new();

    private readonly (string Name, decimal Price)[] _menu =
    {
        ("Капучино классический (250 мл)", 180m),
        ("Паста Карбонара (320 г)", 420m),
        ("Салат Цезарь с курицей (240 г)", 360m),
        ("Чизкейк Нью-Йорк (150 г)", 250m),
        ("Лимонад цитрусовый (400 мл)", 210m)
    };

    public HallOrdersForm()
    {
        Text = "Форма 2 — Зал кафе и расчет заказа — Журба И. С. (Вариант 10)";
        Size = new Size(760, 540);
        StartPosition = FormStartPosition.CenterParent;
        BackColor = Color.FromArgb(248, 250, 252);
        Font = new Font("Segoe UI", 10.5f);

        var titleLabel = new Label
        {
            Text = "Формирование заказа столика (Модуль «Кафе» — Вариант 10)",
            Dock = DockStyle.Top,
            Height = 48,
            TextAlign = ContentAlignment.MiddleLeft,
            Padding = new Padding(18, 0, 0, 0),
            BackColor = Color.FromArgb(44, 62, 80),
            ForeColor = Color.White,
            Font = new Font("Segoe UI Semibold", 13f, FontStyle.Bold)
        };

        var controlsPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 125,
            Padding = new Padding(18, 14, 18, 10),
            BackColor = Color.FromArgb(236, 240, 245)
        };

        _cmbTable = new ComboBox { Width = 140, DropDownStyle = ComboBoxStyle.DropDownList };
        for (int i = 1; i <= 10; i++) _cmbTable.Items.Add($"Столик № {i}");
        _cmbTable.SelectedIndex = 0;

        _cmbDish = new ComboBox { Width = 310, DropDownStyle = ComboBoxStyle.DropDownList };
        foreach (var item in _menu) _cmbDish.Items.Add($"{item.Name} — {item.Price} руб.");
        _cmbDish.SelectedIndex = 0;

        _numPortions = new NumericUpDown { Width = 75, Minimum = 1, Maximum = 20, Value = 2 };
        _chkServiceDiscount = new CheckBox { Text = "Скидка постоянного гостя (10%)", AutoSize = true, Padding = new Padding(6, 4, 0, 0) };

        var btnAddDish = new Button
        {
            Text = "Добавить в заказ",
            Width = 165,
            Height = 36,
            BackColor = Color.FromArgb(39, 134, 78),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
        btnAddDish.Click += OnAddDishClick;

        var btnClear = new Button
        {
            Text = "Очистить список",
            Width = 145,
            Height = 36,
            BackColor = Color.FromArgb(127, 140, 141),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
        btnClear.Click += (_, _) =>
        {
            _lstOrderItems.Items.Clear();
            _lineTotals.Clear();
            UpdateTotalLabel();
        };

        var btnBack = new Button
        {
            Text = "Вернуться на главную форму",
            Width = 240,
            Height = 36,
            BackColor = Color.FromArgb(40, 116, 166),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
        btnBack.Click += (_, _) => Close();

        controlsPanel.Controls.Add(new Label { Text = "Столик:", AutoSize = true, Padding = new Padding(0, 6, 4, 0) });
        controlsPanel.Controls.Add(_cmbTable);
        controlsPanel.Controls.Add(new Label { Text = "Блюдо:", AutoSize = true, Padding = new Padding(8, 6, 4, 0) });
        controlsPanel.Controls.Add(_cmbDish);
        controlsPanel.Controls.Add(new Label { Text = "Порций:", AutoSize = true, Padding = new Padding(8, 6, 4, 0) });
        controlsPanel.Controls.Add(_numPortions);
        controlsPanel.Controls.Add(_chkServiceDiscount);
        controlsPanel.Controls.Add(btnAddDish);
        controlsPanel.Controls.Add(btnClear);
        controlsPanel.Controls.Add(btnBack);

        _chkServiceDiscount.CheckedChanged += (_, _) => UpdateTotalLabel();

        _lstOrderItems = new ListBox
        {
            Dock = DockStyle.Fill,
            Font = new Font("Consolas", 11f),
            IntegralHeight = false
        };

        _lblTotalResult = new Label
        {
            Dock = DockStyle.Bottom,
            Height = 52,
            BackColor = Color.FromArgb(225, 235, 245),
            Font = new Font("Segoe UI Semibold", 12f, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleLeft,
            Padding = new Padding(18, 0, 0, 0),
            Text = "Итого к оплате: 0.00 руб."
        };

        var bodyPanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(18) };
        bodyPanel.Controls.Add(_lstOrderItems!);

        Controls.Add(bodyPanel);
        Controls.Add(_lblTotalResult);
        Controls.Add(controlsPanel);
        Controls.Add(titleLabel);

        // Добавим стартовую позицию для наглядности
        OnAddDishClick(this, EventArgs.Empty);
    }

    private void OnAddDishClick(object? sender, EventArgs e)
    {
        var dish = _menu[_cmbDish.SelectedIndex];
        int qty = (int)_numPortions.Value;
        decimal sum = dish.Price * qty;
        _lineTotals.Add(sum);
        _lstOrderItems.Items.Add($"{_cmbTable.SelectedItem} | {dish.Name} x {qty} шт. = {sum:F2} руб.");
        UpdateTotalLabel();
    }

    private void UpdateTotalLabel()
    {
        decimal rawTotal = _lineTotals.Sum();
        decimal finalTotal = _chkServiceDiscount.Checked ? rawTotal * 0.90m : rawTotal;
        _lblTotalResult.Text = $"Позиций: {_lineTotals.Count} | Сумма: {rawTotal:F2} руб. | Итого к оплате: {finalTotal:F2} руб.";
    }
}
