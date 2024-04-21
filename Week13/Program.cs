using System.Data.SQLite;
using System.Runtime.Versioning;

ReadData(CreateConnection());
//InsertCustomer(CreateConnection());
RemoveCustomer(CreateConnection());

static SQLiteConnection CreateConnection()
{
    SQLiteConnection connection = new SQLiteConnection("Data Source=C:\\Users\\rists\\Desktop\\TKTK KMT2023\\1-K Programmeerimise algkursus - kevad 2024 - J. Voronetskaja\\Week13\\mydb.db; Version=3; New=True; Compress=True;");

    try
    {
        connection.Open();
        Console.WriteLine("DB found.");
    }
    catch
    {
        Console.WriteLine("DB not found.");
    }

    return connection;
}

static void ReadData(SQLiteConnection myConnection)
{
    Console.Clear();
    SQLiteDataReader reader;
    SQLiteCommand cmd;

    cmd = myConnection.CreateCommand();
    cmd.CommandText = "SELECT rowid, * FROM customer";

    reader = cmd.ExecuteReader();

    while (reader.Read())
    {
        string readerRowId = reader["rowid"].ToString();
        string readerStringFirstName = reader.GetString(1);
        string readerStringLastName = reader.GetString(2);
        string readerStringStatus = reader.GetString(3);

        Console.WriteLine($"{readerRowId}. Full name: {readerStringFirstName} {readerStringLastName}; Sünniaeg: {readerStringStatus}");
    }

    myConnection.Close();

}

static void InsertCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand cmd;
    string fName, lName, dob;

    Console.WriteLine("Sisesta oma eesnimi:");
    fName = Console.ReadLine();
    Console.WriteLine("Sisesta oma perekonnanimi:");
    lName = Console.ReadLine();
    Console.WriteLine("Sisesta oma sünniaeg (kk-pp-aaaa):");
    dob = Console.ReadLine();

    cmd = myConnection.CreateCommand();
    cmd.CommandText = $"INSERT INTO customer(firstName, lastName, dateOfBirth) " +
        $"VALUES ('{fName}', '{lName}', '{dob}')";

    int rowInserted = cmd.ExecuteNonQuery();
    Console.WriteLine($"Ridu lisatud: {rowInserted}");

    ReadData(myConnection);

}

static void RemoveCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand cmd;

    Console.WriteLine("Sisesta rea id mille soovid kustutada:");
    string idToDelete = Console.ReadLine();

    cmd = myConnection.CreateCommand();
    cmd.CommandText = $"DELETE FROM customer WHERE rowid = {idToDelete}";
    int rowRemoved = cmd.ExecuteNonQuery();
    Console.WriteLine($"{rowRemoved} eemaldati nimekirjast.");

    ReadData(myConnection);
}
