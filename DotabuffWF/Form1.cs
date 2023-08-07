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
            // �������� DataGridView ����� ����������� ������ �������
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

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

            // �������� �������� � �������
            dataGridView1.Columns.Add("Column1", "��������");
            dataGridView1.Columns.Add("Column2", $"{firstEnemyStr}");
            dataGridView1.Columns.Add("Column3", $"{secondEnemyStr}");
            dataGridView1.Columns.Add("Column4", $"{thirdEnemyStr}");
            dataGridView1.Columns.Add("Column5", $"{fourthEnemyStr}");
            dataGridView1.Columns.Add("Column4", "��������� Disadventage");
            dataGridView1.Columns.Add("Column5", "��������� Winrate");
            // ��������� ������ ��������
            dataGridView1.Columns[0].Width = 130;
            dataGridView1.Columns[1].Width = 130;
            dataGridView1.Columns[2].Width = 130;
            dataGridView1.Columns[3].Width = 130;
            dataGridView1.Columns[4].Width = 130;
            dataGridView1.Columns[5].Width = 105;
            dataGridView1.Columns[6].Width = 100;
            // ���������� �� ������� � ��������� Disadventage
            dataGridView1.Columns[5].SortMode = DataGridViewColumnSortMode.Automatic;

            // ���������� �������
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
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}