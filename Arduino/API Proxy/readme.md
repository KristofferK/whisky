Unfortunately our Arduino isn't able to connect to HTTPS. Our Azure website will only allow HTTPS.

For now, we will have a PHP file hosted on a HTTP site, that will call the Azure API.

## Usage
Send a GET request to arduino_proxy?auth=<BASE64>&pressure=PRESSURE&temperature=TEMPERATURE&sensorId=SENSOR_ID

**Example**
```
double temperature = 70;
int pressure = 2000;
String sensorId = "3";
String auth = "dXNlcm5hbWU6cGFzc3dvcmQ=";

String query = "?auth=" + String(auth) + "&pressure=" + String(pressure) + "&temperature=" + String(temperature) + "&sensorId" + String(sensorId);

if (client.connect(server, 80)) { // if you get a connection, report back via serial:
  Serial.println("connected");
  // Make a HTTP request:
  client.println("GET /arduino_proxy.php" + String(query) + " HTTP/1.1");
  client.println("Host: andet.fobr.dk");
  client.println("User-Agent: Arduino");
  client.println();
} else {
  // if you didn't get a connection to the server:
  Serial.println("connection failed");
}
```