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

        /// <summary>
        /// Создание словарей со статистикой с выбранными персонажами из ComboBox
        /// </summary>
        /// <param name="character">Имя персонажа</param>
        /// <returns></returns>
        public static async Task<Dictionary<string, List<float>>> MakeEnemyDictAsync(string enemyStr)
        {
            
            // Создание ссылки
            string url = Parser.UrlCreator(enemyStr);
            // Парсинг статистики в словарь
            Dictionary<string, List<float>> enemy = await Parser.ParsingAsync(url);
            return enemy;
        }

        /// <summary>
        /// Создание и настройка таблицы
        /// </summary>
        /// <param name="table"></param>
        /// <param name="firstEnemyStr"></param>
        /// <param name="secondEnemyStr"></param>
        /// <param name="thirdEnemyStr"></param>
        /// <param name="fourthEnemyStr"></param>
        public static void DataGreedViewCreator(DataGridView table, string firstEnemyStr, string secondEnemyStr, string thirdEnemyStr, string fourthEnemyStr)
        {
            // Создание столбцов в первой таблице
            table.Columns.Add("Column1", "Персонаж");
            table.Columns.Add("Column2", $"{firstEnemyStr}");
            table.Columns.Add("Column3", $"{secondEnemyStr}");
            table.Columns.Add("Column4", $"{thirdEnemyStr}");
            table.Columns.Add("Column5", $"{fourthEnemyStr}");
            table.Columns.Add("Column6", "Суммарный Disadventage");
            table.Columns.Add("Column7", "Суммарный Winrate");

            // Настройка ширины столбцов в первой таблице
            table.Columns[0].Width = 130;
            table.Columns[1].Width = 130;
            table.Columns[2].Width = 130;
            table.Columns[3].Width = 130;
            table.Columns[4].Width = 130;
            table.Columns[5].Width = 105;

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // Очистить DataGridView перед заполнением новыми данными
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();

            // Преобразование персонажа из comboBox в string
            string? firstEnemyStr = comboBox1.SelectedItem?.ToString();
            string? secondEnemyStr = comboBox2.SelectedItem?.ToString();
            string? thirdEnemyStr = comboBox3.SelectedItem?.ToString();
            string? fourthEnemyStr = comboBox4.SelectedItem?.ToString();

            // Создание словарей со статистикой
            Dictionary<string, List<float>> firstEnemy = await MakeEnemyDictAsync(firstEnemyStr);
            Dictionary<string, List<float>> secondEnemy = await MakeEnemyDictAsync(secondEnemyStr);
            Dictionary<string, List<float>> thirdEnemy = await MakeEnemyDictAsync(thirdEnemyStr);
            Dictionary<string, List<float>> fourthEnemy = await MakeEnemyDictAsync(fourthEnemyStr);

            // Вычисление результата
            Dictionary<string, List<float>> result = new Dictionary<string, List<float>>();
            try
            {
                result = Parser.CharacterStats(firstEnemy, secondEnemy, thirdEnemy, fourthEnemy, firstEnemyStr, secondEnemyStr, thirdEnemyStr, fourthEnemyStr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            // Создание первой таблицы
            DataGreedViewCreator(dataGridView1, firstEnemyStr, secondEnemyStr, thirdEnemyStr, fourthEnemyStr);
            // Создание второй таблицы
            DataGreedViewCreator(dataGridView2, firstEnemyStr, secondEnemyStr, thirdEnemyStr, fourthEnemyStr);

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