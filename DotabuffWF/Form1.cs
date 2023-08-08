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
        /// �������� �������� �� ����������� � ���������� ����������� �� ComboBox
        /// </summary>
        /// <param name="character">��� ���������</param>
        /// <returns></returns>
        public static async Task<Dictionary<string, List<float>>> MakeEnemyDictAsync(string enemyStr)
        {

            // �������� ������
            string url = Parser.UrlCreator(enemyStr);
            // ������� ���������� � �������
            Dictionary<string, List<float>> enemy = await Parser.ParsingAsync(url);
            return enemy;
        }

        /// <summary>
        /// �������� � ��������� �������
        /// </summary>
        /// <param name="table"></param>
        /// <param name="firstEnemyStr"></param>
        /// <param name="secondEnemyStr"></param>
        /// <param name="thirdEnemyStr"></param>
        /// <param name="fourthEnemyStr"></param>
        public static void DataGreedViewCreator(DataGridView table, string firstEnemyStr, string secondEnemyStr, string thirdEnemyStr, string fourthEnemyStr, string fifthEnemyStr)
        {
            // �������� �������� � ������ �������
            table.Columns.Add("Column1", "��������");
            table.Columns.Add("Column2", $"{firstEnemyStr}");
            table.Columns.Add("Column3", $"{secondEnemyStr}");
            table.Columns.Add("Column4", $"{thirdEnemyStr}");
            table.Columns.Add("Column5", $"{fourthEnemyStr}");
            table.Columns.Add("Column6", $"{fifthEnemyStr}");
            table.Columns.Add("Column7", "��������� Disadventage");
            table.Columns.Add("Column8", "��������� Winrate");

            // ��������� ������ �������� � ������ �������
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
            // �������� DataGridView ����� ����������� ������ �������
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();

            // �������������� ��������� �� comboBox � string
            string? firstEnemyStr = comboBox1.SelectedItem?.ToString();
            string? secondEnemyStr = comboBox2.SelectedItem?.ToString();
            string? thirdEnemyStr = comboBox3.SelectedItem?.ToString();
            string? fourthEnemyStr = comboBox4.SelectedItem?.ToString();
            string? fifthEnemyStr = comboBox5.SelectedItem?.ToString();

            // �������� �������� �� �����������
            Dictionary<string, List<float>> firstEnemy = await MakeEnemyDictAsync(firstEnemyStr);
            Dictionary<string, List<float>> secondEnemy = await MakeEnemyDictAsync(secondEnemyStr);
            Dictionary<string, List<float>> thirdEnemy = await MakeEnemyDictAsync(thirdEnemyStr);
            Dictionary<string, List<float>> fourthEnemy = await MakeEnemyDictAsync(fourthEnemyStr);
            Dictionary<string, List<float>> fifthEnemy = await MakeEnemyDictAsync(fifthEnemyStr);

            // ���������� ����������
            Dictionary<string, List<float>> result = new Dictionary<string, List<float>>();
            try
            {
                result = Parser.CharacterStats(firstEnemy, secondEnemy, thirdEnemy, fourthEnemy, fifthEnemy, firstEnemyStr, secondEnemyStr, thirdEnemyStr, fourthEnemyStr, fifthEnemyStr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            // �������� ������ �������
            DataGreedViewCreator(dataGridView1, firstEnemyStr, secondEnemyStr, thirdEnemyStr, fourthEnemyStr, fifthEnemyStr);
            // �������� ������ �������
            DataGreedViewCreator(dataGridView2, firstEnemyStr, secondEnemyStr, thirdEnemyStr, fourthEnemyStr, fifthEnemyStr);

            // ���������� ������ �������
            foreach (var character in result)
            {
                // �������� ������ � ���������� ����� �� ����������
                DataGridViewRow row = new DataGridViewRow();

                DataGridViewTextBoxCell keyCell = new DataGridViewTextBoxCell();
                keyCell.Value = character.Key; // �������� ����� �������
                row.Cells.Add(keyCell);

                foreach (var value in character.Value)
                {
                    DataGridViewTextBoxCell cell = new DataGridViewTextBoxCell();
                    cell.Value = value;
                    row.Cells.Add(cell);
                }

                dataGridView1.Rows.Add(row);
            }

            // TODO: ������� ������ � JSON � ���������� ���������� ����� Windows Forms
            // ������ ��������� ����������
            List<string> selectedCharacters = new List<string>() { "spirit breaker", "techies", "clinkz", "snapfire",
                "meepo", "zeus", "legion commander", "timbersaw", "ogre magi", "lich", "crystal maiden", "undying",
                "witch doctor", "sand king", "dark seer", "juggernaut", "phantom assasin", "anti-mage" };

            // ���������� ������ �������
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // ��������� �������� � ������ �������
                foreach (string character in selectedCharacters)
                {
                    if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == character)
                    {
                        // ������� ����� ������ � �������� ������ �� ������ ������� DataGridView
                        DataGridViewRow newRow = (DataGridViewRow)row.Clone();
                        for (int i = 0; i < row.Cells.Count; i++)
                        {
                            newRow.Cells[i].Value = row.Cells[i].Value;
                        }

                        // ��������� ����� ������ �� ������ DataGridView
                        dataGridView2.Rows.Add(newRow);
                    }
                }
            }

            // ���������� �� ������� � ��������� Disadventage
            dataGridView1.Sort(dataGridView1.Columns[6], ListSortDirection.Descending);
            dataGridView2.Sort(dataGridView2.Columns[6], ListSortDirection.Descending);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void InitializeComboBox()
        {
            // �������� ������ ���� ������ � ComboBox
            comboBox1.DataSource = new List<string>(Characters.characters);
            comboBox2.DataSource = new List<string>(Characters.characters);
            comboBox3.DataSource = new List<string>(Characters.characters);
            comboBox4.DataSource = new List<string>(Characters.characters);
            comboBox5.DataSource = new List<string>(Characters.characters);
            comboBox6.DataSource = new List<string>(Characters.characters);

            // �������� + � ComboBox
            comboBox7.DataSource = new List<string>() { "+" };
            comboBox8.DataSource = new List<string>() { "+" };
            comboBox9.DataSource = new List<string>() { "+" };
            comboBox10.DataSource = new List<string>() { "+" };
            comboBox11.DataSource = new List<string>() { "+" };
            // ������� comboBox
            ComboBoxClear();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // ������� comboBox
            ComboBoxClear();
        }
        private void button4_Click_1(object sender, EventArgs e)
        {
            // ������� comboBox
            ComboBoxClear();
        }
        // ������� comboBox
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
            // �������� DataGridView ����� ����������� ������ �������
            dataGridView3.Rows.Clear();
            dataGridView3.Columns.Clear();

            // �������������� ��������� �� comboBox � string
            string? heroStr = comboBox6.SelectedItem?.ToString();
            string? firstPozitionStr = comboBox7.SelectedItem?.ToString();
            string? secondPozitionStr = comboBox8.SelectedItem?.ToString();
            string? thirdPozitionStr = comboBox9.SelectedItem?.ToString();
            string? fourthPozitionStr = comboBox10.SelectedItem?.ToString();
            string? fifthPozitionStr = comboBox11.SelectedItem?.ToString();
        }
        private void InitializeJsonData()
        {
            // �������� �������� � �������
            dataGridView3.Columns.Add("Column1", "��������");
            dataGridView3.Columns.Add("Column2", $"������ �������");
            dataGridView3.Columns.Add("Column3", $"������ �������");
            dataGridView3.Columns.Add("Column4", $"������ �������");
            dataGridView3.Columns.Add("Column5", $"��������� �������");
            dataGridView3.Columns.Add("Column6", $"����� �������");
            //dataGridView3.Columns.Add("Column7", "��������� Disadventage");
            //dataGridView3.Columns.Add("Column8", "��������� Winrate");

            // ��������� ������ �������� � �������
            dataGridView3.Columns[0].Width = 130;
            dataGridView3.Columns[1].Width = 100;
            dataGridView3.Columns[2].Width = 100;
            dataGridView3.Columns[3].Width = 100;
            dataGridView3.Columns[4].Width = 100;
            dataGridView3.Columns[5].Width = 100;

            // �������������� ��� �������� JSON �����
            Dictionary<string, List<string>> heroPositions = new Dictionary<string, List<string>>();
            string ProductsFileName = "YoursHeroes.json";
            try
            {
                using (var file = new StreamReader(ProductsFileName))
                {
                    string jsonData = file.ReadToEnd();

                    //if (string.IsNullOrWhiteSpace(jsonData)) // ���� ���� �������� ������
                    //{
                    //    heroPositions = new List<string>();
                    //}
                    // �������������� ������
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
            catch (System.IO.FileNotFoundException ex) // ���� ����� �� ����������
            {
                //MessageBox.Show(ex.Message);

                // ���� �� ����������, ������� ����� ����
                using (var file = new StreamWriter(ProductsFileName))
                {
                    string emptyJsonData = JsonConvert.SerializeObject(heroPositions, Formatting.Indented);
                    file.Write(emptyJsonData);
                }
            }

            // ���������� ������� ������������������ �������
            foreach (var character in heroPositions)
            {
                // �������� ������ � ���������� ����� �� ����������
                DataGridViewRow row = new DataGridViewRow();

                DataGridViewTextBoxCell keyCell = new DataGridViewTextBoxCell();
                keyCell.Value = character.Key; // �������� ����� �������
                row.Cells.Add(keyCell);

                foreach (var value in character.Value)
                {
                    DataGridViewTextBoxCell cell = new DataGridViewTextBoxCell();
                    cell.Value = value;
                    row.Cells.Add(cell);
                }

                dataGridView3.Rows.Add(row);
            }
            // ���������� �� �������� ����� ���������
            //dataGridView3.Sort(dataGridView3.Columns[0], ListSortDirection.Descending);
        }

    }
}