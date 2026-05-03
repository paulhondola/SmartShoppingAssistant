# Insomnia exports

This folder contains import-ready Insomnia exports generated from the Postman collections in `postman/json/`.

## Files

- `collections/Category CRUD.insomnia.json`
- `collections/Product CRUD.insomnia.json`
- `collections/Promotion CRUD.insomnia.json`

Each export includes the `Local` environment values from `postman/environments/local.environment.yaml`:

- `baseUrl = http://localhost:5120`
- `productId = 1`
- `categoryId = 1`

## Import

In Insomnia Desktop, use **File → Import/Export → Import Data** and select one of the `.insomnia.json` files.
