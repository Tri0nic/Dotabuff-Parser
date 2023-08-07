using Dotabuff_Parser;
using System.ComponentModel;

namespace DotabuffWF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeComboBox();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // Очистить DataGridView перед заполнением новыми данными
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();

            // Получение выбранных героев
            string firstEnemyStr = comboBox1.SelectedItem.ToString();
            string secondEnemyStr = comboBox2.SelectedItem.ToString();
            string thirdEnemyStr = comboBox3.SelectedItem.ToString();
            string fourthEnemyStr = comboBox4.SelectedItem.ToString();

            // Создание ссылок
            string firstUrl = Parser.UrlCreator(firstEnemyStr);
            string secondUrl = Parser.UrlCreator(secondEnemyStr);
            string thirdUrl = Parser.UrlCreator(thirdEnemyStr);
            string fourthUrl = Parser.UrlCreator(fourthEnemyStr);

            // Парсинг в словари
            Dictionary<string, List<float>> firstEnemy = await Parser.ParsingAsync(firstUrl);
            Dictionary<string, List<float>> secondEnemy = await Parser.ParsingAsync(secondUrl);
            Dictionary<string, List<float>> thirdEnemy = await Parser.ParsingAsync(thirdUrl);
            Dictionary<string, List<float>> fourthEnemy = await Parser.ParsingAsync(fourthUrl);

            // Вычисление результата
            Dictionary<string, List<float>> result = Parser.CharacterStats(firstEnemy, secondEnemy, thirdEnemy, fourthEnemy, firstEnemyStr, secondEnemyStr, thirdEnemyStr, fourthEnemyStr);

            // Создание столбцов в первой таблице
            dataGridView1.Columns.Add("Column1", "Персонаж");
            dataGridView1.Columns.Add("Column2", $"{firstEnemyStr}");
            dataGridView1.Columns.Add("Column3", $"{secondEnemyStr}");
            dataGridView1.Columns.Add("Column4", $"{thirdEnemyStr}");
            dataGridView1.Columns.Add("Column5", $"{fourthEnemyStr}");
            dataGridView1.Columns.Add("Column6", "Суммарный Disadventage");
            dataGridView1.Columns.Add("Column7", "Суммарный Winrate");
            // Настройка ширины столбцов в первой таблице
            dataGridView1.Columns[0].Width = 130;
            dataGridView1.Columns[1].Width = 130;
            dataGridView1.Columns[2].Width = 130;
            dataGridView1.Columns[3].Width = 130;
            dataGridView1.Columns[4].Width = 130;
            dataGridView1.Columns[5].Width = 105;
            dataGridView1.Columns[6].Width = 100;
            // Создание столбцов во второй таблице
            dataGridView2.Columns.Add("Column1", "Персонаж");
            dataGridView2.Columns.Add("Column2", $"{firstEnemyStr}");
            dataGridView2.Columns.Add("Column3", $"{secondEnemyStr}");
            dataGridView2.Columns.Add("Column4", $"{thirdEnemyStr}");
            dataGridView2.Columns.Add("Column5", $"{fourthEnemyStr}");
            dataGridView2.Columns.Add("Column6", "Суммарный Disadventage");
            dataGridView2.Columns.Add("Column7", "Суммарный Winrate");
            // Настройка ширины столбцов во второй таблице
            dataGridView2.Columns[0].Width = 130;
            dataGridView2.Columns[1].Width = 130;
            dataGridView2.Columns[2].Width = 130;
            dataGridView2.Columns[3].Width = 130;
            dataGridView2.Columns[4].Width = 130;
            dataGridView2.Columns[5].Width = 105;
            dataGridView2.Columns[6].Width = 100;

            // Заполнение первой таблицы
            foreach (var character in result)
            {
                // Создание строки и добавление ячеек со значениями
                DataGridViewRow row = new DataGridViewRow();

                DataGridViewTextBoxCell keyCell = new DataGridViewTextBoxCell();
                keyCell.Value = character.Key; // Значение ключа словаря
                row.Cells.Add(keyCell);

                foreach (var value in character.Value)
                {
                    DataGridViewTextBoxCell cell = new DataGridViewTextBoxCell();
                    cell.Value = value;
                    row.Cells.Add(cell);
                }

                dataGridView1.Rows.Add(row);
            }

            // TODO: сделать запись в JSON и добавление персонажей через Windows Forms
            // Список избранных персонажей
            List<string> selectedCharacters = new List<string>() { "spirit breaker", "techies", "clinkz", "snapfire",
                "meepo", "zeus", "legion commander", "timbersaw", "ogre magi", "lich", "crystal maiden", "undying",
                "witch doctor", "sand king", "dark seer", "juggernaut", "phantom assasin", "anti-mage" };

            // Заполнение второй таблицы
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Проверяем значение в первом столбце
                foreach (string character in selectedCharacters)
                {
                    if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == character)
                    {
                        // Создаем новую строку и копируем ячейки из строки первого DataGridView
                        DataGridViewRow newRow = (DataGridViewRow)row.Clone();
                        for (int i = 0; i < row.Cells.Count; i++)
                        {
                            newRow.Cells[i].Value = row.Cells[i].Value;
                        }

                        // Добавляем новую строку во второй DataGridView
                        dataGridView2.Rows.Add(newRow);
                    }
                }
            }

            // Сортировка по столбцу с суммарным Disadventage
            dataGridView1.Sort(dataGridView1.Columns[5], ListSortDirection.Descending);
            dataGridView2.Sort(dataGridView2.Columns[5], ListSortDirection.Descending);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void InitializeComboBox()
        {
            // Привязка списка имен героев к ComboBox
            comboBox1.DataSource = new List<string>(Characters.characters);
            comboBox2.DataSource = new List<string>(Characters.characters);
            comboBox3.DataSource = new List<string>(Characters.characters);
            comboBox4.DataSource = new List<string>(Characters.characters);
            // Очистка comboBox
            ComboBoxClear();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Очистка comboBox
            ComboBoxClear();
        }
        // Очистка comboBox
        public void ComboBoxClear()
        {
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
        }
    }
}