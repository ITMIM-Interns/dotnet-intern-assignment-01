# dotnet-intern-assignment-01
## Assignment 01 – Order Processing API
**Məqsəd:** Coding skills ucun ilkin teessurat yaradan mini web api yaratmaq

---

## 1) API Endpoints

### 1.1 Create Order
**POST** `/orders`

**Request body (JSON):**
```json
{
  "customerName": "Test NAme",
  "product": "Laptop",
  "quantity": 2,
  "price": 1500
}
```

**Validation rules:**
- `customerName` —  (required)
- `product` — (required)
- `quantity` — >0
- `price` — >0

**Response:**
- `201 Created` — (created order)

### 1.2 Get Orders (Pagination ilə)
**GET** `/orders?page=1&pageSize=10`

**Requirements:**
- Pagination dəstəklənməlidir
- Cavabda həm total count, həm də data olmalıdır

### 1.3 Get Order by Id
**GET** `/orders/{id}`

**Response:**
- `200 OK` — order varsa
- `404 Not Found` — order yoxdusa

---

## 2) Data Storage
- Entity Framework Core istifadə olunmalıdır
- InMemory Database istifadə olunmalıdır (real DB tələb olunmur)

**Order entity field-ləri:**
- `Id`
- `CustomerName`
- `Product`
- `Quantity`
- `Price`
- `Status` (Pending, Completed)
- `CreatedAt`

---

## 3) Background Order Processing
Order yaradıldıqda:
- Order DB-yə `Status = Pending` ilə yazılmalıdır

Background process order processing-i simulyasiya etməlidir:
- 5 saniyə sonra order status-u `Completed` olmalıdır

**İstifadə olunacaq:**
- `IHostedService` və ya `BackgroundService`

---

## 4) Logging
`ILogger` should be
- Yeni order yaradıldıqda
- Order background process tərəfindən “processed” olunduqda
- Order tapılmadıqda (404 case)

---

## 5) Error Handling
**Tələblər:**
- Validation xətaları → `400 Bad Request`
- Mövcud olmayan resource → `404 Not Found`
- Unhandled exceptions tətbiqi crash etdirməməlidir
