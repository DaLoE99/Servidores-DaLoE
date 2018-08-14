<?php 
include('config.php');
$passnew = $_POST['passnew'];
$pass_final_n = base64_encode(sha1($passnew, true));  
$query = mysql_query("UPDATE usuarios SET password='$pass_final_n' WHERE usuario = '".$_SESSION['usuario']."'");
if($query) 
{
echo '
<script>
alert("Password Changed!");
</script>';
include('index.php');
}
?>