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
        public static void DataGreedViewCreator(DataGridView table, string firstEnemyStr, string secondEnemyStr, string thirdEnemyStr, string fourthEnemyStr)
        {
            // �������� �������� � ������ �������
            table.Columns.Add("Column1", "��������");
            table.Columns.Add("Column2", $"{firstEnemyStr}");
            table.Columns.Add("Column3", $"{secondEnemyStr}");
            table.Columns.Add("Column4", $"{thirdEnemyStr}");
            table.Columns.Add("Column5", $"{fourthEnemyStr}");
            table.Columns.Add("Column6", "��������� Disadventage");
            table.Columns.Add("Column7", "��������� Winrate");

            // ��������� ������ �������� � ������ �������
            table.Columns[0].Width = 130;
            table.Columns[1].Width = 130;
            table.Columns[2].Width = 130;
            table.Columns[3].Width = 130;
            table.Columns[4].Width = 130;
            table.Columns[5].Width = 105;

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

            // �������� �������� �� �����������
            Dictionary<string, List<float>> firstEnemy = await MakeEnemyDictAsync(firstEnemyStr);
            Dictionary<string, List<float>> secondEnemy = await MakeEnemyDictAsync(secondEnemyStr);
            Dictionary<string, List<float>> thirdEnemy = await MakeEnemyDictAsync(thirdEnemyStr);
            Dictionary<string, List<float>> fourthEnemy = await MakeEnemyDictAsync(fourthEnemyStr);

            // ���������� ����������
            Dictionary<string, List<float>> result = new Dictionary<string, List<float>>();
            try
            {
                result = Parser.CharacterStats(firstEnemy, secondEnemy, thirdEnemy, fourthEnemy, firstEnemyStr, secondEnemyStr, thirdEnemyStr, fourthEnemyStr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            // �������� ������ �������
            DataGreedViewCreator(dataGridView1, firstEnemyStr, secondEnemyStr, thirdEnemyStr, fourthEnemyStr);
            // �������� ������ �������
            DataGreedViewCreator(dataGridView2, firstEnemyStr, secondEnemyStr, thirdEnemyStr, fourthEnemyStr);

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
            dataGridView1.Sort(dataGridView1.Columns[5], ListSortDirection.Descending);
            dataGridView2.Sort(dataGridView2.Columns[5], ListSortDirection.Descending);
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
        // ������� comboBox
        public void ComboBoxClear()
        {
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
        }
    }
}