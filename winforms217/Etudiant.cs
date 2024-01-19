using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace winforms217
{
    internal class Etudiant
    {
        private string matricule;
        private string nom;
        private char sexe;
        private string niveau;
        private bool handicaper;
        private string date_naissance;
        private string code_chambre;

        public Etudiant() { }
        public Etudiant(string matricule)
        {
            this.matricule = matricule;
            charger_etudiant();
        }

        public Etudiant(string matricule, string nom, char sexe, string niveau, bool handicaper, string date_naissance, string code_chambre)
        {
            this.matricule = matricule;
            this.nom = nom;
            this.sexe = sexe;
            this.niveau = niveau;
            this.handicaper = handicaper;
            this.date_naissance = date_naissance;
            this.code_chambre = code_chambre;
        }

        public void ajouter_etudiant()
        {

            using (SqlConnection connection = DatabaseConfig.GetConnection())
            {
                string insertionQuery = "INSERT INTO Etudiant (matricule, nom, sexe, niveau, handicaper, date_naissance, code_chambre) VALUES (@Matricule, @Nom, @Sexe, @Niveau, @Handicaper, @DateNaissance, @CodeChambre)";
                using (SqlCommand command = new SqlCommand(insertionQuery, connection))
                {

                    command.Parameters.AddWithValue("@Matricule", matricule);
                    command.Parameters.AddWithValue("@Nom", nom);
                    command.Parameters.AddWithValue("@Sexe", sexe);
                    command.Parameters.AddWithValue("@Niveau", niveau);
                    command.Parameters.AddWithValue("@Handicaper", handicaper);
                    command.Parameters.AddWithValue("@DateNaissance", date_naissance);
                    command.Parameters.AddWithValue("@CodeChambre", code_chambre);

                    try
                    {
                        connection.Open();

                        int rowsAffected = command.ExecuteNonQuery();
                        MessageBox.Show("Etudiant enregistre");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        public void charger_etudiant()
        {

            using (SqlConnection connection = DatabaseConfig.GetConnection())
            {
                string selectQuery = "SELECT * FROM Etudiant WHERE matricule = @Matricule";
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Matricule", matricule);

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Vérifier si l'étudiant existe
                            if (reader.Read())
                            {
                                nom = reader["nom"].ToString();
                                sexe = Convert.ToChar(reader["sexe"]);
                                niveau = reader["niveau"].ToString();
                                handicaper = Convert.ToBoolean(reader["handicaper"]);
                                date_naissance = reader["date_naissance"].ToString();
                                code_chambre = reader["code_chambre"].ToString();

                                Console.WriteLine("Étudiant chargé");
                            }
                            else
                            {
                                Console.WriteLine("Étudiant non trouvé dans la base de données.");
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

        public void supprimer_etudiant()
        {

            using (SqlConnection connection = DatabaseConfig.GetConnection())
            {
                string deleteQuery = "DELETE FROM Etudiant WHERE matricule = @Matricule";
                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@Matricule", matricule);

                    try
                    {
                        connection.Open();

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"{rowsAffected} étudiant(s) ont été supprimé(s) avec succès.");
                        }
                        else
                        {
                            Console.WriteLine("Étudiant non trouvé dans la base de données.");
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

        public void attribuer_chambre(string code_chambre)
        {


        }

    }
}
