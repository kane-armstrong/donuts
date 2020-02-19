# Donuts

This repository contains simple examples for:

* Hydrating entities with audit information through middleware (i.e. timestamps and user ids for create/update)
* Handling updates using domain events, and dispatching them in-process (todo)
* Working with Entity Framework Core in a database-first context
* Running SSDT projects in docker
* Using docker compose to run the API and database projects together

## Running the example

* Clone
* `cd` to the repository root (`ls` should include `docker-compose`)
* Run `docker-compose build`
* Run `docker-compose up`

You can connect to the database while compose is up using server `.,1400`, username `sa`, and the password in `Dockerfile.Database`
