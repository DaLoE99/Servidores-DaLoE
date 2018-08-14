<?php
if(!$_POST['enviar']) {
$nombre_de_tu_web = "boombangretro.com"; //Sin http:// o www.
$destinatario= "boombangretro@outlook.com";
$asunto= "Mensaje de ".$_POST['nombre'];
$remitente = "From: contacto@".$nombre_de_tu_web."";
$contenido = $_POST['motivo'];
mail($destinatario, $asunto, $contenido, $remitente);
echo 'Mensaje enviado.';
} else {
echo '
<form method="post" action="contacto.php">
<p align="center">
  Tu nombre: <input name="nombre" type="text" value="" size="40"/>
</p>
<p align="center"><span>
  Motivo de tu mensaje: <br> <textarea name="motivo" rows="15" cols="40"></textarea>
</span></p>
<p align="center"><span>
  <input type="submit" name="enviar" value="Enviar MENSAJE" />
</span> </p></form>
';
}
?> 