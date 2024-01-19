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
    public partial class Chambres : Form
    {
        private string code;
        private int niveau;
        private int nb_lit;
        private string code_batiment;
        private bool est_occupe;

        public Chambres()
        {
            InitializeComponent();
        }
        public Chambres(string code_chambre)
        {
            this.code = code_chambre;
            charger_chambre();
        }
        public Chambres(string code, int niveau, int nb_lit, string code_batiment, bool est_occupe)
        {
            this.code = code;
            this.niveau = niveau;
            this.nb_lit = nb_lit;
            this.code_batiment = code_batiment;
            this.est_occupe = est_occupe;
        }

        public void ajouter_chambre()
        {


            using (SqlConnection connection = DatabaseConfig.GetConnection())
            {

                string query = "INSERT INTO Chambre (code, niveau,nb_lit,est_occupe,code_batiment) VALUES (@Valeur1, @Valeur2,@Valeur3,@Valeur4,@Valeur5)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@Valeur1", code);
                    command.Parameters.AddWithValue("@Valeur2", niveau);
                    command.Parameters.AddWithValue("@Valeur3", nb_lit);
                    command.Parameters.AddWithValue("@Valeur4", est_occupe);
                    command.Parameters.AddWithValue("@Valeur5", code_batiment);

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

        //fonction pour recuperer un batiment en bd et le charger dans le code
        public void charger_chambre()
        {

            using (SqlConnection connection = DatabaseConfig.GetConnection())
            {

                string selectQuery = "SELECT * FROM Chambre WHERE Code = @Code";
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

                                niveau = Convert.ToInt32(reader["niveau"]);
                                nb_lit = Convert.ToInt32(reader["nb_lit"]);
                                code_batiment = reader["code_batiment"].ToString();
                                est_occupe = Convert.ToBoolean(reader["niveau"]);
                                Console.WriteLine("Chambre charger");
                            }
                            else
                            {
                                MessageBox.Show("chambre non trouvé dans la base de données.");
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

        public void mettre_a_jour_chambre(int Niveau, int Nb_lit, bool Est_occupe, string Code_batiment)
        {

            using (SqlConnection connection = DatabaseConfig.GetConnection())
            {
                string updateQuery = "UPDATE Chambre SET niveau = @niveau, nb_lit = @nb_lit, est_occupe = @est_occupe, code_batiment = @code_batiment WHERE code = @code";
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    // 3. Ajouter des paramètres
                    command.Parameters.AddWithValue("@code", code);
                    command.Parameters.AddWithValue("@niveau", Niveau);
                    command.Parameters.AddWithValue("@nb_lit", Nb_lit);
                    command.Parameters.AddWithValue("@est_occupe", Est_occupe);
                    command.Parameters.AddWithValue("@code_batiment", Code_batiment);

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
        public void ajouter_lit()
        {
            if (nb_lit < 2)
            {


                using (SqlConnection connection = DatabaseConfig.GetConnection())
                {
                    string updateQuery = "UPDATE Chambre SET nb_lit = @nb_lit WHERE code = @code";
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        // 3. Ajouter des paramètres
                        command.Parameters.AddWithValue("@nb_lit", (nb_lit + 1));
                        command.Parameters.AddWithValue("@code", code);

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
            else
            {
                MessageBox.Show("Nombre de lit max déjà atteint");
            }
        }

        private void Chambres_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Chambres_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cités_uDataSet4.Chambre' table. You can move, or remove it, as needed.
            this.chambreTableAdapter.Fill(this.cités_uDataSet4.Chambre);

        }
    }
}
