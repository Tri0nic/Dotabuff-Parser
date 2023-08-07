using Dotabuff_Parser;
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

            // Создание столбцов в таблице
            dataGridView1.Columns.Add("Column1", "Персонаж");
            dataGridView1.Columns.Add("Column2", $"{firstEnemyStr}");
            dataGridView1.Columns.Add("Column3", $"{secondEnemyStr}");
            dataGridView1.Columns.Add("Column4", $"{thirdEnemyStr}");
            dataGridView1.Columns.Add("Column5", $"{fourthEnemyStr}");
            dataGridView1.Columns.Add("Column4", "Суммарный Disadventage");
            dataGridView1.Columns.Add("Column5", "Суммарный Winrate");
            // Настройка ширины столбцов
            dataGridView1.Columns[0].Width = 130;
            dataGridView1.Columns[1].Width = 130;
            dataGridView1.Columns[2].Width = 130;
            dataGridView1.Columns[3].Width = 130;
            dataGridView1.Columns[4].Width = 130;
            dataGridView1.Columns[5].Width = 105;
            dataGridView1.Columns[6].Width = 100;
            // Сортировка по столбцу с суммарным Disadventage
            dataGridView1.Columns[5].SortMode = DataGridViewColumnSortMode.Automatic;

            // Заполнение таблицы
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
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}