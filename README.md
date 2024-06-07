# Description of the problem

HTTP body conversion will throw an InvalidOperationException when the request have an empty body.

When using the ASP.NET Core integration, a response is never sent back to the client, **the request will hang**.

With the built-in HTTP type with the default System.Text.Json serializer, a response with status code 500 will be sent back to the client.

With the built-in HTTP type with Newtonsoft as the serializer :

- Sending an empty body will throw and a response with status code 500 will be sent back to the client.
- Sending an empty body with the ``Content-Type: application/json`` allows the conversion to proceed without any error. The AzureFunction will receive a null object.

With the in-process model sending an empty body will create an instance of the object.

As a user migrating from the in-process model to the isolated worker model I expect that no exception is thrown when attempting to convert the body when the request doesn't contain a body and I expect that a response is always sent back to the client.

# Tests

You will find below three different requests. Send each of them to AzureFunction to observe the behavior described above.

``curl -d '{ "Name": "potato" }' -H "Content-Type: application/json" -X POST http://localhost:7021/api/Function1 -v``

``curl -H "Content-Type: application/json" -X POST http://localhost:7021/api/Function1 -v``

``curl -X POST http://localhost:7021/api/Function1 -v``