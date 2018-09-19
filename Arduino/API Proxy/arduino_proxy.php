<?PHP
$data = [
    "Pressure" => $_GET['pressure'] ?? 0,
    "Temperature" => $_GET['temperature'] ?? 0,
    "SensorID" => $_GET['sensorId'] ?? '0',
];

$cred = base64_decode($_GET['auth']);

$req = curl_init();
curl_setopt_array($req, [
    CURLOPT_URL            => "https://whiskyserver.azurewebsites.net/api/Measurement/Add",
    CURLOPT_CUSTOMREQUEST  => "PUT",
    CURLOPT_POSTFIELDS     => json_encode($data),
    CURLOPT_HTTPHEADER     => [ "Content-Type: application/json" ],
    CURLOPT_USERPWD        => $cred,  
    CURLOPT_RETURNTRANSFER => true,
    CURLOPT_FAILONERROR => true
]);

$response = curl_exec($req);

curl_close($req);

echo $response;
?>