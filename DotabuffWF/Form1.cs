using Dotabuff_Parser;
using Newtonsoft.Json;
using System.ComponentModel;

namespace DotabuffWF
{
    public partial class Form1 : Form
    {
        private const string YoursHeroes = "YoursHeroes.json";
        private static Dictionary<string, List<string>> selectedCharacters;
        private string? firstEnemyStr;
        private string? secondEnemyStr;
        private string? thirdEnemyStr;
        private string? fourthEnemyStr;
        private string? fifthEnemyStr;
        public static void Deser3Table()
        {
            using (var file = new StreamReader(YoursHeroes))
            {
                string jsonData = file.ReadToEnd();

                // Десериализация списка
                if (JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(jsonData) is Dictionary<string, List<string>> realHeroPositions)
                {
                    selectedCharacters = realHeroPositions;
                }
                else
                {
                    selectedCharacters = new Dictionary<string, List<string>>();
                }
            }
        }
        public Form1()
        {
            InitializeComponent();
            InitializeComboBox();
            InitializeJsonData();
            Deser3Table();
        }

        /// <summary>
        /// Создание словарей со статистикой с выбранными персонажами из ComboBox
        /// </summary>
        /// <param name="character">Имя персонажа</param>
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

        /// <summary>
        /// Добавление столбцов в третью таблицу
        /// </summary>
        public void SelectedHeroesDataGreedViewCreator()
        {
            // Создание столбцов в таблице
            dataGridView3.Columns.Add("Column1", "Персонаж");
            dataGridView3.Columns.Add("Column2", $"Первая позиция");
            dataGridView3.Columns.Add("Column3", $"Вторая позиция");
            dataGridView3.Columns.Add("Column4", $"Третья позиция");
            dataGridView3.Columns.Add("Column5", $"Четвертая позиция");
            dataGridView3.Columns.Add("Column6", $"Пятая позиция");

            // Настройка ширины столбцов в таблице
            dataGridView3.Columns[0].Width = 130;
            dataGridView3.Columns[1].Width = 100;
            dataGridView3.Columns[2].Width = 100;
            dataGridView3.Columns[3].Width = 100;
            dataGridView3.Columns[4].Width = 100;
            dataGridView3.Columns[5].Width = 100;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            #region Получение данные персонажа
            // Преобразование персонажа из comboBox в string
            firstEnemyStr = comboBox1.SelectedItem?.ToString();
            secondEnemyStr = comboBox2.SelectedItem?.ToString();
            thirdEnemyStr = comboBox3.SelectedItem?.ToString();
            fourthEnemyStr = comboBox4.SelectedItem?.ToString();
            fifthEnemyStr = comboBox5.SelectedItem?.ToString();

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
            #endregion

            // Заполнение первой таблицы
            DataGridView1Refresher(firstEnemy, secondEnemy, thirdEnemy, fourthEnemy, fifthEnemy, firstEnemyStr, secondEnemyStr, thirdEnemyStr, fourthEnemyStr, fifthEnemyStr);
            // Заполнение второй таблицы
            DataGridView2Refresher();

            // Обновление второй таблицы в соответствии с выбранными позициями 
            RadioButtonChanger(radioButton1, 0);
            RadioButtonChanger(radioButton2, 1);
            RadioButtonChanger(radioButton3, 2);
            RadioButtonChanger(radioButton4, 3);
            RadioButtonChanger(radioButton5, 4);

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
            comboBox7.DataSource = new List<string>() { "", "+" };
            comboBox8.DataSource = new List<string>() { "", "+" };
            comboBox9.DataSource = new List<string>() { "", "+" };
            comboBox10.DataSource = new List<string>() { "", "+" };
            comboBox11.DataSource = new List<string>() { "", "+" };
            // Очистка comboBox
            ComboBoxClear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Очистка RadioButton
            RadioButtonClear();

            // Обновление второй таблицы
            DataGridView2Refresher();
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

        // Очистка RadioButton
        public void RadioButtonClear()
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            radioButton5.Checked = false;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            // Преобразование персонажа из comboBox в string
            string? heroStr = comboBox6.SelectedItem?.ToString();
            string? firstPozitionStr = comboBox7.SelectedItem?.ToString();
            string? secondPozitionStr = comboBox8.SelectedItem?.ToString();
            string? thirdPozitionStr = comboBox9.SelectedItem?.ToString();
            string? fourthPozitionStr = comboBox10.SelectedItem?.ToString();
            string? fifthPozitionStr = comboBox11.SelectedItem?.ToString();

            //Создание словаря
            Dictionary<string, List<string>> hero = new Dictionary<string, List<string>>();
            try
            {
                hero = new Dictionary<string, List<string>>
                {{ heroStr, new List<string> { firstPozitionStr, secondPozitionStr, thirdPozitionStr, fourthPozitionStr, fifthPozitionStr } }};
            }
            catch
            {
                MessageBox.Show("Выберите персонажа");
            }

            // Десериализация или создание JSON файла
            //Dictionary<string, List<string>> heroPositions = new Dictionary<string, List<string>>();
            try
            {
                using (var file = new StreamReader(YoursHeroes))
                {
                    string jsonData = file.ReadToEnd();

                    // Десериализация списка
                    if (JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(jsonData) is Dictionary<string, List<string>> realHeroPositions)
                    {
                        selectedCharacters = realHeroPositions;
                    }
                }
            }
            catch // Если файла не существует
            {
                // Файл не существует, создаем новый файл
                using (var file = new StreamWriter(YoursHeroes))
                {
                    string emptyJsonData = JsonConvert.SerializeObject(selectedCharacters, Formatting.Indented);
                    file.Write(emptyJsonData);
                }
            }

            // Добавление в десериализованный словарь новые данные
            if (hero.Count != 0)
            {
                selectedCharacters[hero.Keys.First()] = hero.Values.First();
            }

            // Сереализация
            JSONSerializer();

            // Создание и заполнение таблицы десериализованными данными
            DataGridView3Refresher();

            // Сортировка по алфавиту имени персонажа
            dataGridView3.Sort(dataGridView3.Columns[0], ListSortDirection.Ascending);


        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Очистка comboBox
            ComboBoxClear();
        }

        private void InitializeJsonData()
        {
            //SelectedHeroesDataGreedViewCreator();

            // Десериализация или создание JSON файла
            Dictionary<string, List<string>> heroPositions = new Dictionary<string, List<string>>();
            string ProductsFileName = "YoursHeroes.json";
            try
            {
                using (var file = new StreamReader(ProductsFileName))
                {
                    string jsonData = file.ReadToEnd();

                    // Десериализация списка
                    if (JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(jsonData) is Dictionary<string, List<string>> realHeroPositions)
                    {
                        heroPositions = realHeroPositions;
                    }
                }
            }
            catch (System.IO.FileNotFoundException ex) // Если файла не существует
            {
                // Файл не существует, создаем новый файл
                using (var file = new StreamWriter(ProductsFileName))
                {
                    string emptyJsonData = JsonConvert.SerializeObject(heroPositions, Formatting.Indented);
                    file.Write(emptyJsonData);
                }
            }

            // Заполнение таблицы десериализованными данными
            //foreach (var character in heroPositions)
            //{
            //    // Создание строки и добавление ячеек со значениями
            //    DataGridViewRow row = new DataGridViewRow();
            //
            //    DataGridViewTextBoxCell keyCell = new DataGridViewTextBoxCell();
            //    keyCell.Value = character.Key; // Значение ключа словаря
            //    row.Cells.Add(keyCell);
            //
            //    foreach (var value in character.Value)
            //    {
            //        DataGridViewTextBoxCell cell = new DataGridViewTextBoxCell();
            //        cell.Value = value;
            //        row.Cells.Add(cell);
            //    }
            //
            //    dataGridView3.Rows.Add(row);
            //}


            // Создание и заполнение третьей таблицы десериализованными данными
            DataGridView3Refresher(heroPositions);


            
        }

        private void RadioButtonChanger(RadioButton radioButton, int characterPosition)
        {
            if (radioButton.Checked)
            {
                // Очистить DataGridView перед заполнением новыми данными
                dataGridView2.Rows.Clear();
                dataGridView2.Columns.Clear();

                // Создание второй таблицы
                DataGreedViewCreator(dataGridView2, firstEnemyStr, secondEnemyStr, thirdEnemyStr, fourthEnemyStr, fifthEnemyStr);


                // Используем LINQ для выбора элементов с вторым значением списка равным "+"
                var heroes = selectedCharacters
                    .Where(kv => kv.Value.Count > characterPosition && kv.Value[characterPosition] == "+")
                    .ToDictionary(kv => kv.Key, kv => kv.Value);

                // Заполнение второй таблицы
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    // Проверяем значение в первом столбце
                    foreach (string character in heroes.Keys)
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
            }
        }

        /// <summary>
        /// Обновление данных второй таблицы
        /// </summary>
        public void DataGridView2Refresher()
        {
            // Очистить DataGridView перед заполнением новыми данными
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();

            // Создание второй таблицы
            DataGreedViewCreator(dataGridView2, firstEnemyStr, secondEnemyStr, thirdEnemyStr, fourthEnemyStr, fifthEnemyStr);

            // Заполнение второй таблицы
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Проверяем значение в первом столбце
                foreach (string character in selectedCharacters.Keys)
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
            dataGridView2.Sort(dataGridView2.Columns[6], ListSortDirection.Descending);
        }

        /// <summary>
        /// Обновление данных первой таблицы
        /// </summary>
        public void DataGridView1Refresher(
            Dictionary<string, List<float>> firstEnemy,
            Dictionary<string, List<float>> secondEnemy,
            Dictionary<string, List<float>> thirdEnemy,
            Dictionary<string, List<float>> fourthEnemy,
            Dictionary<string, List<float>> fifthEnemy,
            string firstEnemyStr,
            string secondEnemyStr,
            string thirdEnemyStr,
            string fourthEnemyStr,
            string fifthEnemyStr)
        {
            // Очистить DataGridView перед заполнением новыми данными
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

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

            // Сортировка по столбцу с суммарным Disadventage
            dataGridView1.Sort(dataGridView1.Columns[6], ListSortDirection.Descending);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButtonChanger(radioButton1, 0);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButtonChanger(radioButton2, 1);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            RadioButtonChanger(radioButton3, 2);
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            RadioButtonChanger(radioButton4, 3);
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            RadioButtonChanger(radioButton5, 4);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string selectedCharacter = comboBox6.SelectedItem?.ToString();

            // Удаление
            if (selectedCharacters.ContainsKey(selectedCharacter))
            {
                selectedCharacters.Remove(selectedCharacter);
            }

            // Сохранение изменений в JSON
            JSONSerializer();

            // Создание и заполнение третьей таблицы десериализованными данными
            DataGridView3Refresher();
        }

        // Создание и заполнение третьей таблицы десериализованными данными
        public void DataGridView3Refresher()
        {
            // Очистить DataGridView перед заполнением новыми данными
            dataGridView3.Rows.Clear();
            dataGridView3.Columns.Clear();

            // Создание второй таблицы
            SelectedHeroesDataGreedViewCreator();

            int colorRow = 0;
            foreach (var character in selectedCharacters)
            {
                // Переменные для закраски таблицы
                List<int> cells = new List<int>();
                int colorCell = 0;
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

                    colorCell++;
                    if (value == "+")
                    {
                        cells.Add(colorCell);
                    }
                }

                dataGridView3.Rows.Add(row);
                foreach (var cell in cells)
                {
                    dataGridView3.Rows[colorRow].Cells[cell].Style.BackColor = Color.Green;

                }
                colorRow++;
            }

            // Сортировка имени персонажа по алфавиту
            dataGridView3.Sort(dataGridView3.Columns[0], ListSortDirection.Ascending);
        }

        // Создание и заполнение третьей таблицы десериализованными данными (Перегрузка)
        public void DataGridView3Refresher(Dictionary<string, List<string>> selectedCharacters)
        {
            // Очистить DataGridView перед заполнением новыми данными
            dataGridView3.Rows.Clear();
            dataGridView3.Columns.Clear();

            // Создание второй таблицы
            SelectedHeroesDataGreedViewCreator();

            int colorRow = 0;
            foreach (var character in selectedCharacters)
            {
                // Переменные для закраски таблицы
                List<int> cells = new List<int>();
                int colorCell = 0;
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

                    colorCell++;
                    if (value == "+")
                    {
                        cells.Add(colorCell);
                    }
                }

                dataGridView3.Rows.Add(row);
                foreach(var cell in cells)
                {
                    dataGridView3.Rows[colorRow].Cells[cell].Style.BackColor = Color.Green;

                }
                colorRow++;
            }

            // Сортировка имени персонажа по алфавиту
            dataGridView3.Sort(dataGridView3.Columns[0], ListSortDirection.Ascending);
        }
        public void JSONSerializer()
        {
            // Сереализация
            string stringCharacter = JsonConvert.SerializeObject(selectedCharacters);
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                StringEscapeHandling = StringEscapeHandling.Default // Отключаем Unicode-escape
            };
            string jsonString = JsonConvert.SerializeObject(selectedCharacters, settings);
            jsonString = jsonString.Replace("\\", "");
            jsonString = jsonString.Trim('"');
            using (var file = new StreamWriter(YoursHeroes, false))
            {
                file.Write(jsonString);
            }
        }
    }
}