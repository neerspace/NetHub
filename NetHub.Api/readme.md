# Project Documentation

## Authorization / Registration flow

### How to sign up a new user?

1. At the first, you need to create user.

```http request
POST /v1/user
Content-Type: application/json
```

```json
{ "login": "aspadmin" }
```