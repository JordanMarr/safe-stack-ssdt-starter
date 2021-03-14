module Database
open FSharp.Data.Sql

[<Literal>]
let SsdtPath = __SOURCE_DIRECTORY__ + @"/../../ssdt/SafeTodoDB/bin/Debug/SafeTodoDB.dacpac"

type DB = 
    SqlDataProvider<
        Common.DatabaseProviderTypes.MSSQLSERVER_SSDT, 
        SsdtPath = SsdtPath,
        UseOptionTypes = true
    >

// TO RELOAD SCHEMA: 1) uncomment the line below; 2) save; 3) recomment; 4) save again and wait.
//DB.GetDataContext().``Design Time Commands``.ClearDatabaseSchemaCache

let createContext (connectionString: string) =
    DB.GetDataContext(connectionString)