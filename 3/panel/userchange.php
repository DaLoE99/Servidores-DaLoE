<?php 
include('config.php');
$query = mysql_query("UPDATE usuarios SET tipo_avatar = '".$_POST['keko']."', colores_avatar = '".$_POST['c_traje']."".$_POST['c_piel']."".$_POST['c_cinta']."".$_POST['c_ojos']."000000000000000000000000000000000' WHERE usuario = '".$_SESSION['usuario']."'");
if($query) {
echo '
<script>
alert("Edited Character, Please reload BoomBangRetro Client");
</script>';
include('index.php');
}
?>