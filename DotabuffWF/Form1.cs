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
            // �������� DataGridView ����� ����������� ������ �������
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();

            // ��������� ��������� ������
            string firstEnemyStr = comboBox1.SelectedItem.ToString();
            string secondEnemyStr = comboBox2.SelectedItem.ToString();
            string thirdEnemyStr = comboBox3.SelectedItem.ToString();
            string fourthEnemyStr = comboBox4.SelectedItem.ToString();

            // �������� ������
            string firstUrl = Parser.UrlCreator(firstEnemyStr);
            string secondUrl = Parser.UrlCreator(secondEnemyStr);
            string thirdUrl = Parser.UrlCreator(thirdEnemyStr);
            string fourthUrl = Parser.UrlCreator(fourthEnemyStr);

            // ������� � �������
            Dictionary<string, List<float>> firstEnemy = await Parser.ParsingAsync(firstUrl);
            Dictionary<string, List<float>> secondEnemy = await Parser.ParsingAsync(secondUrl);
            Dictionary<string, List<float>> thirdEnemy = await Parser.ParsingAsync(thirdUrl);
            Dictionary<string, List<float>> fourthEnemy = await Parser.ParsingAsync(fourthUrl);

            // ���������� ����������
            Dictionary<string, List<float>> result = Parser.CharacterStats(firstEnemy, secondEnemy, thirdEnemy, fourthEnemy, firstEnemyStr, secondEnemyStr, thirdEnemyStr, fourthEnemyStr);

            // �������� �������� � ������ �������
            dataGridView1.Columns.Add("Column1", "��������");
            dataGridView1.Columns.Add("Column2", $"{firstEnemyStr}");
            dataGridView1.Columns.Add("Column3", $"{secondEnemyStr}");
            dataGridView1.Columns.Add("Column4", $"{thirdEnemyStr}");
            dataGridView1.Columns.Add("Column5", $"{fourthEnemyStr}");
            dataGridView1.Columns.Add("Column6", "��������� Disadventage");
            dataGridView1.Columns.Add("Column7", "��������� Winrate");
            // ��������� ������ �������� � ������ �������
            dataGridView1.Columns[0].Width = 130;
            dataGridView1.Columns[1].Width = 130;
            dataGridView1.Columns[2].Width = 130;
            dataGridView1.Columns[3].Width = 130;
            dataGridView1.Columns[4].Width = 130;
            dataGridView1.Columns[5].Width = 105;
            dataGridView1.Columns[6].Width = 100;
            // �������� �������� �� ������ �������
            dataGridView2.Columns.Add("Column1", "��������");
            dataGridView2.Columns.Add("Column2", $"{firstEnemyStr}");
            dataGridView2.Columns.Add("Column3", $"{secondEnemyStr}");
            dataGridView2.Columns.Add("Column4", $"{thirdEnemyStr}");
            dataGridView2.Columns.Add("Column5", $"{fourthEnemyStr}");
            dataGridView2.Columns.Add("Column6", "��������� Disadventage");
            dataGridView2.Columns.Add("Column7", "��������� Winrate");
            // ��������� ������ �������� �� ������ �������
            dataGridView2.Columns[0].Width = 130;
            dataGridView2.Columns[1].Width = 130;
            dataGridView2.Columns[2].Width = 130;
            dataGridView2.Columns[3].Width = 130;
            dataGridView2.Columns[4].Width = 130;
            dataGridView2.Columns[5].Width = 105;
            dataGridView2.Columns[6].Width = 100;

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