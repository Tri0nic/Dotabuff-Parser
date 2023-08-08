using Dotabuff_Parser;
using Newtonsoft.Json;
using System.ComponentModel;

namespace DotabuffWF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeComboBox();
            InitializeJsonData();
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
        public static void DataGreedViewCreator(DataGridView table, string firstEnemyStr, string secondEnemyStr, string thirdEnemyStr, string fourthEnemyStr, string fifthEnemyStr)
        {
            // Создание столбцов в первой таблице
            table.Columns.Add("Column1", "Персонаж");
            table.Columns.Add("Column2", $"{firstEnemyStr}");
            table.Columns.Add("Column3", $"{secondEnemyStr}");
            table.Columns.Add("Column4", $"{thirdEnemyStr}");
            table.Columns.Add("Column5", $"{fourthEnemyStr}");
            table.Columns.Add("Column6", $"{fifthEnemyStr}");
            table.Columns.Add("Column7", "Суммарный Disadventage");
            table.Columns.Add("Column8", "Суммарный Winrate");

            // Настройка ширины столбцов в первой таблице
            table.Columns[0].Width = 130;
            table.Columns[1].Width = 104;
            table.Columns[2].Width = 104;
            table.Columns[3].Width = 104;
            table.Columns[4].Width = 104;
            table.Columns[5].Width = 104;
            table.Columns[6].Width = 105;

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
            string? fifthEnemyStr = comboBox5.SelectedItem?.ToString();

            // Создание словарей со статистикой
            Dictionary<string, List<float>> firstEnemy = await MakeEnemyDictAsync(firstEnemyStr);
            Dictionary<string, List<float>> secondEnemy = await MakeEnemyDictAsync(secondEnemyStr);
            Dictionary<string, List<float>> thirdEnemy = await MakeEnemyDictAsync(thirdEnemyStr);
            Dictionary<string, List<float>> fourthEnemy = await MakeEnemyDictAsync(fourthEnemyStr);
            Dictionary<string, List<float>> fifthEnemy = await MakeEnemyDictAsync(fifthEnemyStr);

            // Вычисление результата
            Dictionary<string, List<float>> result = new Dictionary<string, List<float>>();
            try
            {
                result = Parser.CharacterStats(firstEnemy, secondEnemy, thirdEnemy, fourthEnemy, fifthEnemy, firstEnemyStr, secondEnemyStr, thirdEnemyStr, fourthEnemyStr, fifthEnemyStr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            // Создание первой таблицы
            DataGreedViewCreator(dataGridView1, firstEnemyStr, secondEnemyStr, thirdEnemyStr, fourthEnemyStr, fifthEnemyStr);
            // Создание второй таблицы
            DataGreedViewCreator(dataGridView2, firstEnemyStr, secondEnemyStr, thirdEnemyStr, fourthEnemyStr, fifthEnemyStr);

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
            dataGridView1.Sort(dataGridView1.Columns[6], ListSortDirection.Descending);
            dataGridView2.Sort(dataGridView2.Columns[6], ListSortDirection.Descending);
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
            comboBox5.DataSource = new List<string>(Characters.characters);
            comboBox6.DataSource = new List<string>(Characters.characters);

            // Привязка + к ComboBox
            comboBox7.DataSource = new List<string>() { "+" };
            comboBox8.DataSource = new List<string>() { "+" };
            comboBox9.DataSource = new List<string>() { "+" };
            comboBox10.DataSource = new List<string>() { "+" };
            comboBox11.DataSource = new List<string>() { "+" };
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
        private void button4_Click_1(object sender, EventArgs e)
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
            comboBox5.SelectedIndex = -1;
            comboBox6.SelectedIndex = -1;
            comboBox7.SelectedIndex = -1;
            comboBox8.SelectedIndex = -1;
            comboBox9.SelectedIndex = -1;
            comboBox10.SelectedIndex = -1;
            comboBox11.SelectedIndex = -1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Очистить DataGridView перед заполнением новыми данными
            dataGridView3.Rows.Clear();
            dataGridView3.Columns.Clear();

            // Преобразование персонажа из comboBox в string
            string? heroStr = comboBox6.SelectedItem?.ToString();
            string? firstPozitionStr = comboBox7.SelectedItem?.ToString();
            string? secondPozitionStr = comboBox8.SelectedItem?.ToString();
            string? thirdPozitionStr = comboBox9.SelectedItem?.ToString();
            string? fourthPozitionStr = comboBox10.SelectedItem?.ToString();
            string? fifthPozitionStr = comboBox11.SelectedItem?.ToString();
        }
        private void InitializeJsonData()
        {
            // Создание столбцов в таблице
            dataGridView3.Columns.Add("Column1", "Персонаж");
            dataGridView3.Columns.Add("Column2", $"Первая позиция");
            dataGridView3.Columns.Add("Column3", $"Вторая позиция");
            dataGridView3.Columns.Add("Column4", $"Третья позиция");
            dataGridView3.Columns.Add("Column5", $"Четвертая позиция");
            dataGridView3.Columns.Add("Column6", $"Пятая позиция");
            //dataGridView3.Columns.Add("Column7", "Суммарный Disadventage");
            //dataGridView3.Columns.Add("Column8", "Суммарный Winrate");

            // Настройка ширины столбцов в таблице
            dataGridView3.Columns[0].Width = 130;
            dataGridView3.Columns[1].Width = 100;
            dataGridView3.Columns[2].Width = 100;
            dataGridView3.Columns[3].Width = 100;
            dataGridView3.Columns[4].Width = 100;
            dataGridView3.Columns[5].Width = 100;

            // Десериализация или создание JSON файла
            Dictionary<string, List<string>> heroPositions = new Dictionary<string, List<string>>();
            string ProductsFileName = "YoursHeroes.json";
            try
            {
                using (var file = new StreamReader(ProductsFileName))
                {
                    string jsonData = file.ReadToEnd();

                    //if (string.IsNullOrWhiteSpace(jsonData)) // Если файл окажется пустым
                    //{
                    //    heroPositions = new List<string>();
                    //}
                    // Десериализация списка
                    if (JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(jsonData) is Dictionary<string, List<string>> realHeroPositions)
                    {
                        heroPositions = realHeroPositions;
                    }
                    //else
                    //{
                    //    heroPositions = new List<string>();
                    //}
                }
            }
            catch (System.IO.FileNotFoundException ex) // Если файла не существует
            {
                //MessageBox.Show(ex.Message);

                // Файл не существует, создаем новый файл
                using (var file = new StreamWriter(ProductsFileName))
                {
                    string emptyJsonData = JsonConvert.SerializeObject(heroPositions, Formatting.Indented);
                    file.Write(emptyJsonData);
                }
            }

            // Заполнение таблицы десериализованными данными
            foreach (var character in heroPositions)
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

                dataGridView3.Rows.Add(row);
            }
            // Сортировка по алфавиту имени персонажа
            //dataGridView3.Sort(dataGridView3.Columns[0], ListSortDirection.Descending);
        }

    }
}