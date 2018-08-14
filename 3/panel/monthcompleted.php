<?php 
include('config.php');
$query = mysql_query("UPDATE usuarios SET tiempo_registrado= '".$_POST['monthnew']."' WHERE usuario = '".$_SESSION['usuario']."'");
if($query) {
echo '
<script>
alert("Months Edited! Please reload BoomBangRetro Client");
alert("Meses Editado! Por favor, vuelva a cargar BoomBangRetro cliente");
</script>';
include('index.php');
}
?>