<center>
<p><p>
	<table border="3" width="600"  cellspacing="1" >
	<h2> <font="26"> Uppercut Leaderboard</size></h2>
	<td><center>Rank</td>
	<td><center>Username</td>
	<td><center>Uppercuts Sent</td>
	<td><center>Uppercut Receive</td>
	<td><center>Credits</td>
<tr>

<?php
include ("dbsettings.php"); 

	mysql_connect($dbhost, $dbuser, $dbpw) OR
	die("ERROR: Connection failed. ".mysql_error());		
	mysql_select_db($db) OR
	die("ERROR: DB allready open. ".mysql_error());
	$test = "SELECT * from usuarios";
		$testquery = mysql_query($test);
			$num2 = mysql_num_rows($testquery);

			$i = 0;

	$acc = mysql_query("SELECT * from usuarios order by uppercuts_enviados desc limit 100");
	while($row = mysql_fetch_object($acc)) {
		$name = $row->usuario;
		$upper = $row->uppercuts_enviados;
		$guante = $row->nivel_ring;
		$upper1 = $row->uppercuts_recibidos;
		$credits = $row->creditos_oro;
			$i = $i + 1;
			echo "
	<td> <center>$i </td>
	<td> <center>$name </td>
	<td> <center>$upper </td>
	<td><center> $upper1 </td>
	<td> <center>$credits </td><tr>
";	}
			 

	
?>

