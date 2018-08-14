/*
Navicat MySQL Data Transfer

Source Server         : http://5.109.20.176/
Source Server Version : 50508
Source Host           : 127.0.0.1:3306
Source Database       : boombang

Target Server Type    : MYSQL
Target Server Version : 50508
File Encoding         : 65001

Date: 2011-12-16 00:56:25
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `amigos`
-- ----------------------------
DROP TABLE IF EXISTS `amigos`;
CREATE TABLE `amigos` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_usuario` int(11) NOT NULL,
  `id_amigo` int(11) NOT NULL,
  `aceptado` enum('0','1') NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of amigos
-- ----------------------------

-- ----------------------------
-- Table structure for `areas_privadas`
-- ----------------------------
DROP TABLE IF EXISTS `areas_privadas`;
CREATE TABLE `areas_privadas` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_parent` int(11) NOT NULL,
  `nombre` varchar(30) NOT NULL,
  `descripcion` varchar(250) NOT NULL DEFAULT 'Añade una descripción... :)',
  `id_area1` int(11) NOT NULL DEFAULT '-1',
  `id_area2` int(11) NOT NULL DEFAULT '-1',
  `id_area3` int(11) NOT NULL DEFAULT '-1',
  `id_area4` int(11) NOT NULL DEFAULT '-1',
  `id_area5` int(11) NOT NULL DEFAULT '-1',
  `id_usuario` int(11) NOT NULL,
  `permitir_uppercuts` enum('0','1') NOT NULL DEFAULT '0',
  `permitir_cocos` enum('0','1') NOT NULL DEFAULT '1',
  `lista_verde` text NOT NULL,
  `lista_negra` text NOT NULL,
  PRIMARY KEY (`id`),
  KEY `nombre` (`nombre`),
  KEY `id_usuario` (`id_usuario`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of areas_privadas
-- ----------------------------

-- ----------------------------
-- Table structure for `areas_publicas`
-- ----------------------------
DROP TABLE IF EXISTS `areas_publicas`;
CREATE TABLE `areas_publicas` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_principal` int(11) NOT NULL,
  `nombre` varchar(20) NOT NULL,
  `modelo_area` int(3) NOT NULL DEFAULT '1' COMMENT '1 = belugaBeach, 2 = U.F.O. 3 = Minikong...',
  `max_visitantes` int(3) NOT NULL DEFAULT '12',
  `categoria` tinyint(1) NOT NULL DEFAULT '1' COMMENT '1 = juego individual, 2 = juego en grupo',
  `permitir_uppercut` enum('0','1') NOT NULL DEFAULT '0',
  `permitir_coco` enum('0','1') NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of areas_publicas
-- ----------------------------
INSERT INTO `areas_publicas` VALUES ('1', '0', 'Skate Parck', '16', '16', '1', '1', '1');
INSERT INTO `areas_publicas` VALUES ('2', '0', 'Iglo', '9', '16', '1', '0', '0');
INSERT INTO `areas_publicas` VALUES ('3', '0', 'U.F.O', '2', '16', '1', '0', '0');
INSERT INTO `areas_publicas` VALUES ('4', '0', 'MiniKong', '3', '12', '1', '0', '0');
INSERT INTO `areas_publicas` VALUES ('5', '0', 'BaoBab', '4', '12', '1', '0', '0');
INSERT INTO `areas_publicas` VALUES ('6', '0', 'Media Noche', '5', '12', '1', '0', '0');
INSERT INTO `areas_publicas` VALUES ('7', '0', 'Vèrtigo', '6', '12', '1', '0', '0');
INSERT INTO `areas_publicas` VALUES ('8', '0', 'IceAge', '7', '12', '1', '0', '0');

-- ----------------------------
-- Table structure for `catalogo_categorias`
-- ----------------------------
DROP TABLE IF EXISTS `catalogo_categorias`;
CREATE TABLE `catalogo_categorias` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(30) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of catalogo_categorias
-- ----------------------------
INSERT INTO `catalogo_categorias` VALUES ('1', 'Objetos decorativos');
INSERT INTO `catalogo_categorias` VALUES ('2', 'Set bosque');
INSERT INTO `catalogo_categorias` VALUES ('3', 'Mascotas y cosas parecidas');
INSERT INTO `catalogo_categorias` VALUES ('4', 'Set miedo');
INSERT INTO `catalogo_categorias` VALUES ('5', 'Set indio');
INSERT INTO `catalogo_categorias` VALUES ('6', 'Set hielo');
INSERT INTO `catalogo_categorias` VALUES ('7', 'Regalos');
INSERT INTO `catalogo_categorias` VALUES ('8', 'San valentín');
INSERT INTO `catalogo_categorias` VALUES ('9', 'Set alien');
INSERT INTO `catalogo_categorias` VALUES ('10', 'Set pirata');
INSERT INTO `catalogo_categorias` VALUES ('11', 'Set oriental');
INSERT INTO `catalogo_categorias` VALUES ('12', 'Set magia');
INSERT INTO `catalogo_categorias` VALUES ('13', 'Set pascua');
INSERT INTO `catalogo_categorias` VALUES ('14', 'Set casa');
INSERT INTO `catalogo_categorias` VALUES ('15', 'Set plantas');

-- ----------------------------
-- Table structure for `catalogo_objetos`
-- ----------------------------
DROP TABLE IF EXISTS `catalogo_objetos`;
CREATE TABLE `catalogo_objetos` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` int(11) NOT NULL,
  `descripcion` int(11) NOT NULL,
  `pagina_catalogo` int(11) NOT NULL,
  `precio_oro` int(11) NOT NULL,
  `precio_plata` int(11) NOT NULL,
  `colores` int(11) NOT NULL,
  `size` int(11) NOT NULL,
  `rotacion` int(11) NOT NULL,
  `pivotes_ocupa` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `pagina_catalogo` (`pagina_catalogo`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of catalogo_objetos
-- ----------------------------

-- ----------------------------
-- Table structure for `objetos_comprados`
-- ----------------------------
DROP TABLE IF EXISTS `objetos_comprados`;
CREATE TABLE `objetos_comprados` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_objeto` int(11) NOT NULL,
  `id_area` int(11) NOT NULL DEFAULT '0',
  `id_usuario` int(11) NOT NULL,
  `size` varchar(5) NOT NULL DEFAULT 'tam_n',
  `color` varchar(60) NOT NULL,
  `pivotes_ocupa` int(11) NOT NULL,
  `pos_x` int(11) NOT NULL DEFAULT '0',
  `pos_y` int(11) NOT NULL DEFAULT '0',
  `pos_z` int(11) NOT NULL DEFAULT '0',
  `data_extra` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `id_area` (`id_area`),
  KEY `id_usuario` (`id_usuario`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of objetos_comprados
-- ----------------------------

-- ----------------------------
-- Table structure for `server_status`
-- ----------------------------
DROP TABLE IF EXISTS `server_status`;
CREATE TABLE `server_status` (
  `usuarios_online` int(11) NOT NULL DEFAULT '0',
  `areas_activas` int(11) NOT NULL DEFAULT '0',
  `servidor_online` enum('0','1') NOT NULL DEFAULT '0' COMMENT '0 = Offline, 1 = Online'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of server_status
-- ----------------------------
INSERT INTO `server_status` VALUES ('0', '9', '1');

-- ----------------------------
-- Table structure for `usuarios`
-- ----------------------------
DROP TABLE IF EXISTS `usuarios`;
CREATE TABLE `usuarios` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `usuario` varchar(13) NOT NULL,
  `password` varchar(21) NOT NULL,
  `email` varchar(40) NOT NULL DEFAULT 'default@jairo34.es',
  `edad` int(2) NOT NULL DEFAULT '14',
  `tiempo_registrado` int(3) NOT NULL,
  `pais` varchar(30) NOT NULL DEFAULT 'Alemania',
  `ciudad` varchar(30) NOT NULL DEFAULT 'BoomBang',
  `ip_registro` varchar(16) NOT NULL DEFAULT '127.0.0.1',
  `ip_actual` varchar(16) NOT NULL DEFAULT '127.0.0.1',
  `ultimo_login` varchar(19) NOT NULL DEFAULT '1970-01-01 00:00:00',
  `moderador` enum('0','2','3','1') NOT NULL DEFAULT '0' COMMENT '0 = No es moderador, 1 = Si es moderador',
  `tipo_avatar` int(2) NOT NULL,
  `colores_avatar` varchar(42) NOT NULL DEFAULT 'FFD797CC5806FFFFFF6633000066CCFFFFFF000000',
  `bocadillo` varchar(30) NOT NULL DEFAULT 'Hola! :)',
  `hobby_1` varchar(15) DEFAULT NULL,
  `hobby_2` varchar(15) DEFAULT NULL,
  `hobby_3` varchar(15) DEFAULT NULL,
  `deseo_1` varchar(15) DEFAULT NULL,
  `deseo_2` varchar(15) DEFAULT NULL,
  `deseo_3` varchar(15) DEFAULT NULL,
  `votos_simpatico` tinyint(2) NOT NULL DEFAULT '50',
  `votos_sexy` tinyint(2) NOT NULL DEFAULT '50',
  `votos_legal` tinyint(2) NOT NULL DEFAULT '50',
  `creditos_oro` int(11) NOT NULL DEFAULT '100000000',
  `creditos_plata` int(11) NOT NULL DEFAULT '5000',
  `besos_enviados` int(11) NOT NULL DEFAULT '0',
  `cocteles_enviados` int(11) NOT NULL DEFAULT '0',
  `flores_enviadas` int(11) NOT NULL DEFAULT '0',
  `uppercuts_enviados` int(11) NOT NULL DEFAULT '0',
  `cocos_enviados` int(11) NOT NULL DEFAULT '0',
  `besos_recibidos` int(11) NOT NULL DEFAULT '0',
  `cocteles_recibidos` int(11) NOT NULL DEFAULT '0',
  `flores_recibidas` int(11) NOT NULL DEFAULT '0',
  `uppercuts_recibidos` int(11) NOT NULL DEFAULT '0',
  `cocos_recibidos` int(11) NOT NULL DEFAULT '0',
  `ring` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `usuario` (`usuario`),
  KEY `email` (`email`)
) ENGINE=InnoDB AUTO_INCREMENT=43 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of usuarios
-- ----------------------------
