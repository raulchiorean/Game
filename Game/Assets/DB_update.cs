using System.IO;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;

public class DB_update : MonoBehaviour
{
    IDbConnection conn;//




    /// <summary>
    ///  : gameObject.GetComponent<DB_update>."numefunctie"(param 1, param2, etc...);
    /// 
    /// </summary>
    /// <param name="dbName"></param>
    // connect to db;
    public void db_connect(string dbName)
    {
        string nume_database = "//" + dbName + ".s3db";
        string connection = "URI=file:" + Application.dataPath + nume_database;
        this.conn = new SqliteConnection(connection);
        this.conn.Open();
    }

    //update strength for a specific name
    public void strength_update_byName(int new_strength, string name, string dbName, string tableName)
    {
        db_connect(dbName);
        IDbCommand db_strength_update = this.conn.CreateCommand();
        db_strength_update.CommandText = "UPDATE " + tableName + "SET strength = " + new_strength + " WHERE name = " + name;
        this.conn.Close();
    }

    //update HP for a specific name; 
    public void HP_update_byName(int new_HP, string name, string dbName, string tableName)
    {
        db_connect(dbName);
        IDbCommand db_HP_update = this.conn.CreateCommand();
        db_HP_update.CommandText = "UPDATE " + tableName + " SET HP = " + new_HP + " WHERE name = " + name;
        db_HP_update.ExecuteNonQuery();
        this.conn.Close();
    }

    //add new player to database
    public void db_addPlayer(string name, int strength, int HP,string dbName)
    {
        db_connect(dbName);
        IDbCommand db_addPlayer = this.conn.CreateCommand();
        db_addPlayer.CommandText = "INSERT INTO player_stats(name, strength, HP) VALUES " + "('" + name + "'," + strength + ", " + HP + ")";
        db_addPlayer.ExecuteNonQuery();
        this.conn.Close();
    }

    // delete specific player from database
    public void db_deletePlayer(string name, string dbName)
    {
        db_connect(dbName);
        IDbCommand db_deletePlayer = this.conn.CreateCommand();
        db_deletePlayer.CommandText = "DELETE FROM player_stats WHERE name = " + "'" + name + "';";
        this.conn.Close();
    }

    //delete all entries from a specific table
    public void db_deleteAll(string dbName, string tableName)
    {
        db_connect(dbName);
        IDbCommand db_deleteAll = this.conn.CreateCommand();
        db_deleteAll.CommandText = "DELETE * FROM " + tableName;
        this.conn.Close();
    }

    void Start()
    {

    }


    void Update()
    {

    }
}