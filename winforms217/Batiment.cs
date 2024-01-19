using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace winforms217
{
    public partial class Batiment : Form
    {
        private string code;
        private string libelle;
        private int nb_niveau;
        private int nb_chambre;
        private float prix_chambre;

        public Batiment()
        {
            InitializeComponent();
        }
        public Batiment(string code)
        {
            this.code = code;
            charger_batiment();
        }
        public Batiment(string code, String libelle, int nb_niveau, int nb_chambre,float prix_chambre)
        {
            this.code = code;
            this.libelle = libelle;
            this.nb_niveau = nb_niveau;
            this.nb_chambre = nb_chambre;
            this.prix_chambre = prix_chambre;
        }
        public void ajouter_batiment()
        {
            using (SqlConnection connection = DatabaseConfig.GetConnection() )
            {
                string query = "INSERT INTO Batiment (code, libelle,nb_niveau,nb_chambre,prix_chambre) VALUES (@Valeur1, @Valeur2,@Valeur3,@Valeur4,@Valeur5)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@Valeur1", this.code);
                    command.Parameters.AddWithValue("@Valeur2", this.libelle);
                    command.Parameters.AddWithValue("@Valeur3", this.nb_niveau);
                    command.Parameters.AddWithValue("@Valeur4", this.nb_chambre);
                    command.Parameters.AddWithValue("@Valeur5", this.prix_chambre);

                    try
                    {
                        connection.Open();

                        int nombre_lignes_affectées = command.ExecuteNonQuery();
                        Console.WriteLine($"{nombre_lignes_affectées} lignes ont été insérées avec succès.");

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
                connection.Close();
            }

        }
        public void consulter_etat() { }
        public void ajouter_chambre(int nombre)
        {
            using (SqlConnection connection = DatabaseConfig.GetConnection())
            {

                string updateQuery = "UPDATE Batiment SET nb_chambre = @NouvelleValeur WHERE code = @Condition";
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {

                    command.Parameters.AddWithValue("@NouvelleValeur", (nombre + this.nb_chambre));
                    command.Parameters.AddWithValue("@Condition", this.code);

                    try
                    {
                        connection.Open();
                        int nombres_lignes_affectées = command.ExecuteNonQuery();
                        Console.WriteLine($"{nombres_lignes_affectées} lignes ont été mises à jour avec succès.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erreur : {ex.Message}");
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        //fonction pour recuperer un batiment en bd et le charger dans le code
        public void charger_batiment()
        {

            using (SqlConnection connection = DatabaseConfig.GetConnection())
            {

                string selectQuery = "SELECT * FROM Batiment WHERE code = @Code";
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {

                    command.Parameters.AddWithValue("@Code", code);

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // 6. Vérifier si le bâtiment existe
                            if (reader.Read())
                            {

                                libelle = reader["libelle"].ToString();
                                nb_niveau = Convert.ToInt32(reader["nb_niveau"]);
                                nb_chambre = Convert.ToInt32(reader["nb_chambre"]);
                                prix_chambre = Convert.ToSingle(reader["prix_chambre"]);
                                Console.WriteLine("Batiment charger");
                            }
                            else
                            {
                                Console.WriteLine("Bâtiment non trouvé dans la base de données.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erreur : {ex.Message}");
                    }
                    finally
                    {

                        connection.Close();
                    }
                }
            }
        }

        public void mettre_a_jour_batiment(string code, String libelle, int nb_niveau, int nb_chambre, float prix_chambre)
        {

            using (SqlConnection connection = DatabaseConfig.GetConnection())
            {
                string updateQuery = "UPDATE Batiment SET libelle = @Libelle, nb_niveau = @NbNiveau, nb_chambre = @NbChambre, prix_chambre = @PrixChambre WHERE code = @Code";
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    // 3. Ajouter des paramètres
                    command.Parameters.AddWithValue("@Code", code);
                    command.Parameters.AddWithValue("@Libelle", libelle);
                    command.Parameters.AddWithValue("@NbNiveau", nb_niveau);
                    command.Parameters.AddWithValue("@NbChambre", nb_chambre);
                    command.Parameters.AddWithValue("@PrixChambre", prix_chambre);

                    try
                    {
                        // 4. Ouvrir la connexion
                        connection.Open();

                        // 5. Exécuter la commande
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"{rowsAffected} lignes ont été mises à jour avec succès.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erreur : {ex.Message}");
                    }
                    finally
                    {
                        // 6. Fermer la connexion
                        connection.Close();
                    }
                }
            }
        }
        private void Batiment_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            //button enregistrer batiments
            int code,chambres = 0;
            float prix;
            int.TryParse(textBox3.Text,out code);
            int.TryParse(textBox4.Text, out chambres);
            float.TryParse(textBox5.Text,out prix);
            Batiment batiment = new Batiment(textBox1.Text,textBox2.Text,code, chambres, prix);
            //enregistrement du batiment dans bd
            batiment.ajouter_batiment();
            //vider les textbox
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";

        }

        private void Batiment_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cités_uDataSet3.Batiment' table. You can move, or remove it, as needed.
            this.batimentTableAdapter.Fill(this.cités_uDataSet3.Batiment);

        }
    }
}
