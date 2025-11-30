using ZeneApp.Common;

namespace ZeneApp.WinForms;

public class ZeneEditorForm : Form
{
    private TextBox txtCim, txtEloado;
    private NumericUpDown numEv, numHossz, numPrioritas;
    private Button btnSave;
    private Zene? _zeneToEdit;
    private bool _isEditMode;

    public ZeneEditorForm(Zene? zene = null)
    {
        _zeneToEdit = zene;
        _isEditMode = zene != null;
        InitializeComponent();
        LoadData();
    }

    private void InitializeComponent()
    {
        this.Size = new Size(400, 400);
        this.Text = _isEditMode ? "Zene Módosítása (Prioritás)" : "Új Zene Hozzáadása";

        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(20) };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60));

        // Segédfüggvény mezők létrehozásához
        void AddField(string label, Control ctrl, int row)
        {
            layout.Controls.Add(new Label { Text = label, AutoSize = true, Anchor = AnchorStyles.Left }, 0, row);
            ctrl.Width = 200;
            layout.Controls.Add(ctrl, 1, row);
        }

        txtCim = new TextBox();
        txtEloado = new TextBox();
        numEv = new NumericUpDown { Minimum = 1900, Maximum = 2100, Value = 2023 };
        numHossz = new NumericUpDown { Minimum = 1, Maximum = 10000 };
        numPrioritas = new NumericUpDown { Minimum = 1, Maximum = 10 };

        AddField("Cím:", txtCim, 0);
        AddField("Előadó:", txtEloado, 1);
        AddField("Kiadás éve:", numEv, 2);
        AddField("Hossz (mp):", numHossz, 3);
        AddField("Prioritás:", numPrioritas, 4);

        btnSave = new Button { Text = "Mentés", DialogResult = DialogResult.None };
        btnSave.Click += BtnSave_Click;
        layout.Controls.Add(btnSave, 1, 5);

        this.Controls.Add(layout);

        // Ha szerkesztünk, tiltsuk le a nem módosítható mezőket
        if (_isEditMode)
        {
            txtCim.Enabled = false;
            txtEloado.Enabled = false;
            numEv.Enabled = false;
            numHossz.Enabled = false;
            // Csak a prioritás marad aktív
        }
    }

    private void LoadData()
    {
        if (_isEditMode && _zeneToEdit != null)
        {
            txtCim.Text = _zeneToEdit.Cim;
            txtEloado.Text = _zeneToEdit.Eloado;
            numEv.Value = _zeneToEdit.KiadasiEv;
            numHossz.Value = _zeneToEdit.Hossz;
            numPrioritas.Value = _zeneToEdit.Prioritas;
        }
    }

    private void BtnSave_Click(object? sender, EventArgs e)
    {
        using var db = new ZeneContext();

        if (_isEditMode && _zeneToEdit != null)
        {
            // Módosítás (csak prioritás)
            var zeneDb = db.Zenek.Find(_zeneToEdit.Id);
            if (zeneDb != null)
            {
                zeneDb.Prioritas = (int)numPrioritas.Value;
                db.SaveChanges();
            }
        }
        else
        {
            // Új hozzáadása
            var ujZene = new Zene
            {
                Cim = txtCim.Text,
                Eloado = txtEloado.Text,
                KiadasiEv = (int)numEv.Value,
                Hossz = (int)numHossz.Value,
                Prioritas = (int)numPrioritas.Value
            };

            // Validáció (egyszerűsített)
            if (string.IsNullOrWhiteSpace(ujZene.Cim) || string.IsNullOrWhiteSpace(ujZene.Eloado))
            {
                MessageBox.Show("Cím és Előadó kötelező!");
                return;
            }
            if (db.Zenek.Any(z => z.Cim == ujZene.Cim))
            {
                MessageBox.Show("Már létezik ilyen című zene!");
                return;
            }

            db.Zenek.Add(ujZene);
            db.SaveChanges();
        }

        this.DialogResult = DialogResult.OK;
        this.Close();
    }
}