module TodoRepository
open FSharp.Data.Sql
open Database
open Shared

/// Get all todos (regardless of IsDone status)
let getTodos (db: DB.dataContext) = 
    query {
        for todo in db.Dbo.Todos do
        select 
            { Shared.Todo.Id = todo.Id
              Shared.Todo.Description = todo.Description
              Shared.Todo.IsDone = todo.IsDone }
    }
    |> List.executeQueryAsync

let addTodo (db: DB.dataContext) (todo: Shared.Todo) =
    async {
        let t = db.Dbo.Todos.Create()
        t.Id <- todo.Id
        t.Description <- todo.Description
        t.IsDone <- false

        do! db.SubmitUpdatesAsync()
    }

let updateTodo (db: DB.dataContext) (todo: Shared.Todo) = 
    async {
        let! existingTodo =
            query { 
                for t in db.Dbo.Todos do
                where (t.Id = todo.Id)
                select t
            }
            |> Seq.tryHeadAsync

        // Fail if this unexpected scenario occurs
        let existingTodo = existingTodo |> Option.defaultWith (fun () -> failwith "Update failed: Todo was deleted!")

        existingTodo.Description <- todo.Description
        existingTodo.IsDone <- todo.IsDone

        do! db.SubmitUpdatesAsync()
        return todo
    }