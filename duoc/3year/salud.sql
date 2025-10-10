-- phpMyAdmin SQL Dump
-- version 4.7.9
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1:3306
-- Tiempo de generación: 13-07-2018 a las 11:59:42
-- Versión del servidor: 5.7.21
-- Versión de PHP: 5.6.35

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `salud`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `analisismuestra`
--

DROP TABLE IF EXISTS `analisismuestra`;
CREATE TABLE IF NOT EXISTS `analisismuestra` (
  `idAnalisis` int(11) NOT NULL AUTO_INCREMENT,
  `fechaRecepcion` varchar(30) NOT NULL,
  `tempMuestra` decimal(10,0) NOT NULL,
  `cantidadMuestra` int(11) NOT NULL,
  `empresa_codigoEmpresa` int(11) DEFAULT NULL,
  `particular_codigoParticular` int(11) DEFAULT NULL,
  `rutEmpleadoRecibe` varchar(10) DEFAULT NULL,
  PRIMARY KEY (`idAnalisis`),
  KEY `rutEmpleadoRecibe` (`rutEmpleadoRecibe`),
  KEY `empresa_codigoEmpresa` (`empresa_codigoEmpresa`),
  KEY `particular_codigoParticular` (`particular_codigoParticular`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `analisismuestra`
--

INSERT INTO `analisismuestra` (`idAnalisis`, `fechaRecepcion`, `tempMuestra`, `cantidadMuestra`, `empresa_codigoEmpresa`, `particular_codigoParticular`, `rutEmpleadoRecibe`) VALUES
(1, '12/07/2018', '4', 2, 2, 1, '12715841-3'),
(2, '13/07/2018', '4', 3, 2, 1, '12715841-3');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `contacto`
--

DROP TABLE IF EXISTS `contacto`;
CREATE TABLE IF NOT EXISTS `contacto` (
  `rutContacto` varchar(12) NOT NULL,
  `nombreContacto` varchar(30) NOT NULL,
  `emailContacto` varchar(45) NOT NULL,
  `telefonoContacto` varchar(15) NOT NULL,
  `empresa_codigoEmpresa` int(11) NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`rutContacto`),
  KEY `empresa_codigoEmpresa` (`empresa_codigoEmpresa`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `contacto`
--

INSERT INTO `contacto` (`rutContacto`, `nombreContacto`, `emailContacto`, `telefonoContacto`, `empresa_codigoEmpresa`) VALUES
('19521506-2', 'Andres Gonzalez', 'a.gonzales@gmail.com', '123231', 1),
('321342', 'Roberto Carlos', 'r.carlos@gmail.com', '32131242', 2);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `empleado`
--

DROP TABLE IF EXISTS `empleado`;
CREATE TABLE IF NOT EXISTS `empleado` (
  `rutEmpleado` varchar(10) NOT NULL,
  `nombreEmpleado` varchar(50) NOT NULL,
  `passwordEmpleado` varchar(10) NOT NULL,
  `categoria` varchar(1) NOT NULL,
  PRIMARY KEY (`rutEmpleado`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `empleado`
--

INSERT INTO `empleado` (`rutEmpleado`, `nombreEmpleado`, `passwordEmpleado`, `categoria`) VALUES
('12125643-3', 'Juan', '12345', 'A'),
('12321', 'tast', 'tast', 'A'),
('12715841-3', 'Sebastian', '12345', 'T'),
('14125683-3', 'Antonio', '12345', 'R');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `empresa`
--

DROP TABLE IF EXISTS `empresa`;
CREATE TABLE IF NOT EXISTS `empresa` (
  `codigoEmpresa` int(11) NOT NULL,
  `rutEmpresa` varchar(10) NOT NULL,
  `nombreEmpresa` varchar(30) NOT NULL,
  `passwordEmpresa` varchar(10) NOT NULL,
  `direccionEmpresa` varchar(50) NOT NULL,
  PRIMARY KEY (`codigoEmpresa`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `empresa`
--

INSERT INTO `empresa` (`codigoEmpresa`, `rutEmpresa`, `nombreEmpresa`, `passwordEmpresa`, `direccionEmpresa`) VALUES
(1, '12312321', 'OneWri', '123456', 'asdad@asdasd.cl'),
(2, '2313123', 'duoc', '123456', 'Puente Alto');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `particular`
--

DROP TABLE IF EXISTS `particular`;
CREATE TABLE IF NOT EXISTS `particular` (
  `codigoParticular` int(11) NOT NULL AUTO_INCREMENT,
  `rutParticular` varchar(45) NOT NULL,
  `passwordParticular` varchar(45) NOT NULL,
  `nombreParticular` varchar(45) NOT NULL,
  `direccionParticular` varchar(45) NOT NULL,
  `emailParticular` varchar(100) NOT NULL,
  `telefono` varchar(12) NOT NULL,
  PRIMARY KEY (`codigoParticular`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `particular`
--

INSERT INTO `particular` (`codigoParticular`, `rutParticular`, `passwordParticular`, `nombreParticular`, `direccionParticular`, `emailParticular`, `telefono`) VALUES
(1, '19521507-3', 'asda123', 'antonio altamirano', 'puente alto', 'asdas@gmail.com', '123212321'),
(2, '19522507-3', 'asdabs123', 'juan perez', 'puente alto', 'asdas@gmail.com', '123212321'),
(3, '', '', '', '', '', ''),
(4, '19521507-3', 'asdasd', 'dasdasd', 'dasdsa', 'dsada', '13245232'),
(5, '', '', '', '', '', ''),
(6, '', '', '', '', '', ''),
(7, '', '', '', '', '', ''),
(8, '', 'asdasd', 'sdadas', 'dsadas', 'dsadasd', '123123'),
(9, '', '', '', '', '', '');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `resultadoanalisis`
--

DROP TABLE IF EXISTS `resultadoanalisis`;
CREATE TABLE IF NOT EXISTS `resultadoanalisis` (
  `idTipoAnalisis` int(11) DEFAULT NULL,
  `idAnalisisMuestras` int(11) DEFAULT NULL,
  `fechaRegistro` varchar(30) NOT NULL,
  `ppm` int(11) NOT NULL,
  `estado` varchar(30) NOT NULL,
  `rutEmpleadoAnalista` varchar(10) DEFAULT NULL,
  KEY `idTipoAnalisis` (`idTipoAnalisis`),
  KEY `idAnalisisMuestras` (`idAnalisisMuestras`),
  KEY `rutEmpleadoAnalista` (`rutEmpleadoAnalista`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipoanallisis`
--

DROP TABLE IF EXISTS `tipoanallisis`;
CREATE TABLE IF NOT EXISTS `tipoanallisis` (
  `idTipoAnalisis` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(45) NOT NULL,
  PRIMARY KEY (`idTipoAnalisis`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `tipoanallisis`
--

INSERT INTO `tipoanallisis` (`idTipoAnalisis`, `nombre`) VALUES
(1, 'Micotoxinas'),
(2, 'metales pesados'),
(3, 'Plaguicidas prohibidos'),
(4, 'Marea roja'),
(5, 'bacterias nocivas'),
(6, 'test');

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `analisismuestra`
--
ALTER TABLE `analisismuestra`
  ADD CONSTRAINT `analisismuestra_ibfk_3` FOREIGN KEY (`rutEmpleadoRecibe`) REFERENCES `empleado` (`rutEmpleado`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `analisismuestra_ibfk_5` FOREIGN KEY (`particular_codigoParticular`) REFERENCES `particular` (`codigoParticular`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `analisismuestra_ibfk_6` FOREIGN KEY (`empresa_codigoEmpresa`) REFERENCES `empresa` (`codigoEmpresa`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `contacto`
--
ALTER TABLE `contacto`
  ADD CONSTRAINT `contacto_ibfk_1` FOREIGN KEY (`empresa_codigoEmpresa`) REFERENCES `empresa` (`codigoEmpresa`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `resultadoanalisis`
--
ALTER TABLE `resultadoanalisis`
  ADD CONSTRAINT `resultadoanalisis_ibfk_1` FOREIGN KEY (`rutEmpleadoAnalista`) REFERENCES `empleado` (`rutEmpleado`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `resultadoanalisis_ibfk_4` FOREIGN KEY (`idTipoAnalisis`) REFERENCES `tipoanallisis` (`idTipoAnalisis`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `resultadoanalisis_ibfk_5` FOREIGN KEY (`idAnalisisMuestras`) REFERENCES `analisismuestra` (`idAnalisis`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
