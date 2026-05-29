[Back to README](../README.md)

### Sale Orders

#### GET /sale-orders
- Description: Retrieve a paginated list of sale orders
- Query Parameters:
  - `currentPage` (optional): Page number for pagination (default: 1)
  - `pageSize` (optional): Number of items per page (default: 20)
  - `includeProductList` (optional): Include products list in each order (default: true)
  - `orderNumberFrom` (optional): Filter orders from this order number
  - `orderNumberTo` (optional): Filter orders up to this order number
  - `customerName` (optional): Filter by customer name (contains)
  - `branchName` (optional): Filter by branch name (contains)
  - `cancelStatus` (optional): Filter by cancellation status (enum: NotCancelled, Cancelled)
  - `dateFrom` (optional): Filter orders from this date
  - `dateTo` (optional): Filter orders up to this date
  - `orderBy` (optional): Sort expression (examples: "orderNumber", "orderNumber asc", "orderNumber desc")
- Response:
  ```json
  {
    "data": {
      "currentPage": "integer",
      "totalPages": "integer",
      "orders": [
        {
          "id": "guid",
          "orderNumber": "integer",
          "date": "datetime",
          "customerName": "string",
          "branchName": "string",
          "totalSale": "decimal",
          "cancelStatus": "string (enum: NotCancelled, Cancelled)",
          "createdAt": "datetime",
          "updatedAt": "datetime",
          "products": [
            {
              "id": "guid",
              "cancelStatus": "string (enum: NotCancelled, Cancelled)",
              "eanGtin": "string",
              "description": "string",
              "price": "decimal",
              "amount": "integer",
              "discount": "integer",
              "totalValue": "decimal"
            }
          ]
        }
      ]
    }
  }
  ```

#### POST /sale-orders
- Description: Create a new sale order
- Request Body:
  ```json
  {
    "customerName": "string",
    "branchName": "string",
    "products": [
      {
        "eanGtin": "string",
        "description": "string",
        "price": "decimal",
        "amount": "integer"
      }
    ]
  }
  ```
- Response:
  ```json
  {
    "data": {
      "id": "guid",
      "orderNumber": "integer",
      "date": "datetime",
      "customerName": "string",
      "branchName": "string",
      "totalSale": "decimal",
      "cancelStatus": "string (enum: NotCancelled, Cancelled)",
      "createdAt": "datetime",
      "updatedAt": "datetime",
      "products": [
        {
          "id": "guid",
          "cancelStatus": "string (enum: NotCancelled, Cancelled)",
          "eanGtin": "string",
          "description": "string",
          "price": "decimal",
          "amount": "integer",
          "discount": "integer",
          "totalValue": "decimal"
        }
      ]
    }
  }
  ```

#### GET /sale-orders/{id}
- Description: Retrieve a specific sale order by ID
- Path Parameters:
  - `id`: Sale order ID
- Response:
  ```json
  {
    "data": {
      "id": "guid",
      "orderNumber": "integer",
      "date": "datetime",
      "customerName": "string",
      "branchName": "string",
      "totalSale": "decimal",
      "cancelStatus": "string (enum: NotCancelled, Cancelled)",
      "createdAt": "datetime",
      "updatedAt": "datetime",
      "products": [
        {
          "id": "guid",
          "cancelStatus": "string (enum: NotCancelled, Cancelled)",
          "eanGtin": "string",
          "description": "string",
          "price": "decimal",
          "amount": "integer",
          "discount": "integer",
          "totalValue": "decimal"
        }
      ]
    }
  }
  ```

#### PUT /sale-orders/{id}
- Description: Update a specific sale order
- Path Parameters:
  - `id`: Sale order ID
- Request Body:
  ```json
  {
    "customerName": "string",
    "branchName": "string",
    "cancelStatus": "string (enum: NotCancelled, Cancelled)",
    "products": [
      {
        "eanGtin": "string",
        "description": "string",
        "price": "decimal",
        "amount": "integer",
        "cancelStatus": "string (enum: NotCancelled, Cancelled)"
      }
    ]
  }
  ```
- Response:
  ```json
  {
    "data": {
      "id": "guid",
      "orderNumber": "integer",
      "date": "datetime",
      "customerName": "string",
      "branchName": "string",
      "totalSale": "decimal",
      "cancelStatus": "string (enum: NotCancelled, Cancelled)",
      "createdAt": "datetime",
      "updatedAt": "datetime",
      "products": [
        {
          "id": "guid",
          "saleOrderId": "guid",
          "cancelStatus": "string (enum: NotCancelled, Cancelled)",
          "eanGtin": "string",
          "description": "string",
          "price": "decimal",
          "amount": "integer",
          "discount": "integer",
          "totalValue": "decimal",
          "createdAt": "datetime",
          "updatedAt": "datetime"
        }
      ]
    }
  }
  ```

#### DELETE /sale-orders/{id}
- Description: Delete a specific sale order
- Path Parameters:
  - `id`: Sale order ID
- Response:
  ```json
  {
    "success": "boolean",
    "message": "string"
  }
  ```

#### Business Rules
- Purchases above 4 identical items have 10% discount
- Purchases between 10 and 20 identical items have 20% discount
- It is not possible to sell above 20 identical items
- Purchases below 4 items cannot have discount
