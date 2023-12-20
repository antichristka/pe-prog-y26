# TodoList API

## Create Task

### Create Task Request

```js
POST /tasks
```

```json
{
  "title": "Buy milk",
  "description": "Mom asked me to buy milk",
  "deadline": "2023-12-31T23:59:59.999Z",
  "tags": ["shopping", "home"],
  "isCompleted": false
}
```

### Create Task Response

```js
201 Created
```

```js
Location: {{host}}/tasks/{{id}}
```

```json
{
  "id": "1",
  "title": "Buy milk",
  "description": "Mom asked me to buy milk",
  "deadline": "2023-12-31T23:59:59.999Z",
  "tags": ["shopping", "home"],
  "isCompleted": false
}
```

## Get Task

### Get Task Request

```js
GET /tasks/{{id}}
```

### Get Task Response

```js
200 OK
```

```json
{
  "id": "00000000-0000-0000-0000-000000000000",
  "title": "Buy milk",
  "description": "Mom asked me to buy milk",
  "deadline": "2023-12-31T23:59:59.999Z",
  "tags": ["shopping", "home"],
  "isCompleted": false
}
```

## Update Task

### Update Task Request

```js
PUT /tasks/{{id}}
```

```json
{
  "title": "Buy milk",
  "description": "Mom asked me to buy milk",
  "deadline": "2023-12-31T23:59:59.999Z",
  "tags": ["shopping", "home"],
  "isCompleted": false
}
```

### Update Task Response

```js
204 No Content
```

or

```js
200 OK
```

```js
Location: {{host}}/tasks/{{id}}
```

## Delete Task

### Delete Task Request

```js
DELETE /tasks/{{id}}
```

### Delete Task Response

```js
204 No Content
```
