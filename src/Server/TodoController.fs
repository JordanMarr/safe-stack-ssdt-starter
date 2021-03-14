module TodoController
open Database
open Shared

let getTodos (db: DB.dataContext) = 
    TodoRepository.getTodos db

let addTodo (db: DB.dataContext) (todo: Todo) = 
    async {
        if Todo.isValid todo.Description then
            do! TodoRepository.addTodo db todo
            return todo
        else 
            return failwith "Invalid todo"
    }    

let updateTodo (db: DB.dataContext) (todo: Todo) = 
    async {
        if Todo.isValid todo.Description 
        then return! TodoRepository.updateTodo db todo
        else return failwith "Invalid todo"
    }