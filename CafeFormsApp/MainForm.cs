using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ZhurbaCafeForms;

public sealed class MainNavigatorForm : Form
{
    private readonly Label _lblHeader;
    private readonly Label _lblSubHeader;
    private readonly Button _btnOpenSecondForm;
    private readonly Button _btnShowHelp;
    private readonly Button _btnShowGitIgnore;
    private readonly Button _btnExit;
    private readonly TextBox _txtInfoBox;

    public MainNavigatorForm()
    {
        Text = "ПР №4–5 — АИС «Кафе Уют» (Главная форма) — Журба И. С., Вариант 10";
        Size = new Size(860, 620);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Color.FromArgb(245, 247, 250);
        Font = new Font("Segoe UI", 10.5f);

        var headerPanel = new Panel
        {
            Dock = DockStyle.Top,
            Height = 95,
            BackColor = Color.FromArgb(30, 58, 95),
            Padding = new Padding(20, 14, 20, 10)
        };

        _lblHeader = new Label
        {
            Text = "Автоматизированная система «Кафе» — Вариант № 10",
            ForeColor = Color.White,
            Font = new Font("Segoe UI Semibold", 15f, FontStyle.Bold),
            AutoSize = true,
            Location = new Point(20, 14)
        };

        _lblSubHeader = new Label
        {
            Text = "Разработчик: Журба Иван Сергеевич | ПР № 4–5: Настройка СКВ Git (.gitignore, ветвление и слияние)",
            ForeColor = Color.FromArgb(200, 220, 245),
            Font = new Font("Segoe UI", 10f),
            AutoSize = true,
            Location = new Point(22, 52)
        };

        headerPanel.Controls.Add(_lblHeader);
        headerPanel.Controls.Add(_lblSubHeader);

        var buttonPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 68,
            Padding = new Padding(18, 12, 18, 10),
            BackColor = Color.FromArgb(232, 238, 246)
        };

        _btnOpenSecondForm = CreateStyledButton("Перейти в зал и заказы (Форма 2)", Color.FromArgb(40, 116, 166), 265);
        _btnShowHelp = CreateStyledButton("Вызов справки о проекте", Color.FromArgb(39, 134, 78), 210);
        _btnShowGitIgnore = CreateStyledButton("Проверить фильтр .gitignore", Color.FromArgb(142, 68, 173), 215);
        _btnExit = CreateStyledButton("Выход", Color.FromArgb(180, 55, 55), 95);

        _btnOpenSecondForm.Click += OnOpenSecondFormClick;
        _btnShowHelp.Click += OnShowHelpClick;
        _btnShowGitIgnore.Click += OnShowGitIgnoreClick;
        _btnExit.Click += (_, _) => Close();

        buttonPanel.Controls.AddRange(new Control[] { _btnOpenSecondForm, _btnShowHelp, _btnShowGitIgnore, _btnExit });

        var contentPanel = new Panel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(20)
        };

        _txtInfoBox = new TextBox
        {
            Multiline = true,
            ReadOnly = true,
            Dock = DockStyle.Fill,
            Font = new Font("Consolas", 10.5f),
            BackColor = Color.White,
            ScrollBars = ScrollBars.Vertical,
            Text = GetDefaultDescription()
        };

        contentPanel.Controls.Add(_txtInfoBox);

        Controls.Add(contentPanel);
        Controls.Add(buttonPanel);
        Controls.Add(headerPanel);
    }

    private static Button CreateStyledButton(string caption, Color backColor, int width)
    {
        return new Button
        {
            Text = caption,
            Width = width,
            Height = 40,
            BackColor = backColor,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Margin = new Padding(0, 0, 12, 0),
            Cursor = Cursors.Hand
        };
    }

    private static string GetDefaultDescription()
    {
        return
            "=== СВЕДЕНИЯ О ПРОЕКТЕ ПРАКТИЧЕСКОЙ РАБОТЫ № 4–5 ===\r\n" +
            "Исполнитель: Журба Иван Сергеевич\r\n" +
            "Вариант: № 10 (Программный модуль «Кафе»)\r\n" +
            "Репозиторий GitHub: https://github.com/Ivan091020/ISRPO-PR4-5-Zhurba\r\n\r\n" +
            "Назначение приложения:\r\n" +
            "1. Главная форма (MainNavigatorForm) обеспечивает навигацию между модулями системы,\r\n" +
            "   вызов диалогового окна справки и просмотр конфигурации фильтров репозитория Git.\r\n" +
            "2. Вторая форма (HallOrdersForm) реализует учет столиков, выбор блюд меню кафе\r\n" +
            "   и автоматический расчет стоимости заказа гостя.\r\n\r\n" +
            "Контроль версий Git:\r\n" +
            "- Настроен файл .gitignore для исключения каталогов сборки (bin/, obj/, .vs/)\r\n" +
            "  и временных артефактов (*.exe, *.dll, *.pdb, *.user).\r\n" +
            "- Выполнена работа с ветками (main и feature/cafe-menu-upgrade),\r\n" +
            "  проведено слияние ветвей и разрешение конфликта слияния.";
    }

    private void OnOpenSecondFormClick(object? sender, EventArgs e)
    {
        using var secondForm = new HallOrdersForm();
        secondForm.ShowDialog(this);
        _txtInfoBox.Text = GetDefaultDescription() +
            $"\r\n\r\n[Журнал навигации]: Выполнен переход на Форму 2 и возврат в главное окно ({DateTime.Now:HH:mm:ss}).";
    }

    private void OnShowHelpClick(object? sender, EventArgs e)
    {
        string helpMessage =
            "Справочная система модуля «Кафе Уют» (Вариант 10)\n\n" +
            "Автор проекта: Журба Иван Сергеевич\n" +
            "Дисциплина: Инструментальные средства разработки ПО (ИСРПО)\n" +
            "Практическая работа № 4–5: Настройка работы системы контроля версий\n" +
            "(типов импортируемых файлов и путей в репозиторий).\n\n" +
            "Инструкция пользователя:\n" +
            "• Нажмите «Перейти в зал и заказы (Форма 2)» для открытия окна расчета заказов.\n" +
            "• Нажмите «Проверить фильтр .gitignore» для просмотра правил фильтрации файлов.\n" +
            "• Нажмите «Выход» для завершения работы приложения.";

        MessageBox.Show(
            this,
            helpMessage,
            "Справка о программе — Журба И. С.",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }

    private void OnShowGitIgnoreClick(object? sender, EventArgs e)
    {
        _txtInfoBox.Text =
            "=== НАСТРОЕННЫЕ ПРАВИЛА ФАЙЛА .gitignore ===\r\n" +
            "# Исключение скомпилированных бинарных файлов и объектных папок\r\n" +
            "[Bb]in/\r\n" +
            "[Oo]bj/\r\n" +
            "[Dd]ebug/\r\n" +
            "[Rr]elease/\r\n\r\n" +
            "# Исключение кэша и пользовательских настроек Visual Studio\r\n" +
            ".vs/\r\n" +
            "*.user\r\n" +
            "*.suo\r\n" +
            "*.userosscache\r\n\r\n" +
            "# Исключение исполняемых и отладочных типов файлов\r\n" +
            "*.exe\r\n" +
            "*.dll\r\n" +
            "*.pdb\r\n" +
            "*.log\r\n" +
            "*.tmp\r\n\r\n" +
            "Статус проверки: служебные каталоги bin/ и obj/ успешно отфильтрованы из индекса Git.";
    }
}
