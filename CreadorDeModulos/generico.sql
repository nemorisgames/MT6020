-- phpMyAdmin SQL Dump
-- version 4.5.2
-- http://www.phpmyadmin.net
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 13-05-2016 a las 21:59:31
-- Versión del servidor: 5.7.9
-- Versión de PHP: 5.6.16

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `generico`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `admin`
--

DROP TABLE IF EXISTS `admin`;
CREATE TABLE IF NOT EXISTS `admin` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `user` varchar(50) NOT NULL,
  `pass` varchar(50) NOT NULL,
  `name` varchar(50) DEFAULT NULL,
  `lastName` varchar(50) DEFAULT NULL,
  `email` varchar(50) DEFAULT NULL,
  `phone` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `admin`
--

INSERT INTO `admin` (`id`, `user`, `pass`, `name`, `lastName`, `email`, `phone`) VALUES
(1, 'mgonzalez', 'marko123', 'marco', NULL, NULL, NULL);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `checkmodule`
--

DROP TABLE IF EXISTS `checkmodule`;
CREATE TABLE IF NOT EXISTS `checkmodule` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `fk_moduleType` bigint(20) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_moduleType` (`fk_moduleType`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `checkmoduledetail`
--

DROP TABLE IF EXISTS `checkmoduledetail`;
CREATE TABLE IF NOT EXISTS `checkmoduledetail` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `fk_realizationModule` bigint(20) NOT NULL,
  `fk_checkQuestion` bigint(20) NOT NULL,
  `correctAnswer` tinyint(1) NOT NULL,
  `answerMade` tinyint(1) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_realizationModule` (`fk_realizationModule`),
  KEY `fk_checkQuestion` (`fk_checkQuestion`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `checkquestions`
--

DROP TABLE IF EXISTS `checkquestions`;
CREATE TABLE IF NOT EXISTS `checkquestions` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `fk_ckeckModule` bigint(20) NOT NULL,
  `questionName` text NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_ckeckModule` (`fk_ckeckModule`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `designtype`
--

DROP TABLE IF EXISTS `designtype`;
CREATE TABLE IF NOT EXISTS `designtype` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `designtype`
--

INSERT INTO `designtype` (`id`, `name`) VALUES
(6, 'diseno1'),
(7, 'diseno2'),
(8, 'diseno3'),
(9, 'diseno4'),
(10, 'diseno5'),
(11, 'diseno6');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `detail`
--

DROP TABLE IF EXISTS `detail`;
CREATE TABLE IF NOT EXISTS `detail` (
  `id` bigint(20) NOT NULL,
  `VariableName` varchar(100) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `VariableName` (`VariableName`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `informationmoduleanswers`
--

DROP TABLE IF EXISTS `informationmoduleanswers`;
CREATE TABLE IF NOT EXISTS `informationmoduleanswers` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `fk_informationModuleQuestions` bigint(20) NOT NULL,
  `text` text NOT NULL,
  `correct` varchar(20) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_informationModuleQUestions` (`fk_informationModuleQuestions`)
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `informationmoduleanswers`
--

INSERT INTO `informationmoduleanswers` (`id`, `fk_informationModuleQuestions`, `text`, `correct`) VALUES
(6, 7, 'asdsad', 'False'),
(7, 7, 'asdasd', 'False'),
(8, 7, 'ver1', 'True'),
(11, 6, 'RVERDADERA1', 'True'),
(13, 6, 'RFALSA1', 'False'),
(14, 6, 'RFALSA2', 'False'),
(15, 6, 'RFALSA3', 'False'),
(16, 6, 'RFALSA4', 'False'),
(17, 6, 'RFALSA5', 'False'),
(18, 7, 'qwerwqr', 'False'),
(19, 7, 'qwerwqr2', 'False'),
(20, 8, 'jkjnjkjkjk', 'True'),
(21, 8, 'jkjkklk', 'False'),
(22, 8, 'jkjkklk', 'False'),
(23, 8, 'jkjkklk', 'True'),
(24, 8, 'jkjkkjkjl', 'False');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `informationmoduledetail`
--

DROP TABLE IF EXISTS `informationmoduledetail`;
CREATE TABLE IF NOT EXISTS `informationmoduledetail` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `fk_realizationModule` bigint(20) NOT NULL,
  `fk_questionID` bigint(20) NOT NULL,
  `fk_informationModuleAnswers` bigint(20) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_realizationModule` (`fk_realizationModule`),
  KEY `fk_realizateAnswer` (`fk_informationModuleAnswers`)
) ENGINE=InnoDB AUTO_INCREMENT=38 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `informationmoduledetail`
--

INSERT INTO `informationmoduledetail` (`id`, `fk_realizationModule`, `fk_questionID`, `fk_informationModuleAnswers`) VALUES
(1, 1, 7, 8),
(2, 1, 7, 8),
(3, 1, 7, 8),
(4, 1, 7, 8),
(5, 1, 7, 8),
(6, 1, 7, 8),
(7, 1, 7, 8),
(8, 1, 7, 6),
(9, 1, 7, 8),
(10, 1, 7, 6),
(11, 1, 7, 6),
(12, 1, 7, 8),
(13, 1, 7, 8),
(14, 1, 7, 8),
(15, 1, 7, 8),
(16, 1, 7, 19),
(17, 1, 7, 8),
(18, 1, 7, 8),
(19, 1, 7, 8),
(20, 1, 7, 8),
(21, 1, 7, 11),
(22, 1, 7, 8),
(23, 1, 7, 17),
(24, 1, 7, 8),
(25, 1, 7, 17),
(26, 1, 7, 8),
(27, 1, 7, 11),
(28, 1, 7, 19),
(29, 1, 7, 11),
(30, 1, 7, 6),
(31, 1, 7, 14),
(32, 1, 7, 19),
(33, 1, 7, 13),
(34, 1, 7, 19),
(35, 1, 8, 15),
(36, 1, 8, 19),
(37, 1, 8, 20);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `informationmodulequestion`
--

DROP TABLE IF EXISTS `informationmodulequestion`;
CREATE TABLE IF NOT EXISTS `informationmodulequestion` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `fk_module` bigint(20) NOT NULL,
  `question` varchar(300) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_informationModule` (`fk_module`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `informationmodulequestion`
--

INSERT INTO `informationmodulequestion` (`id`, `fk_module`, `question`) VALUES
(6, 10, 'Who are you'),
(7, 10, 'Pregunta 2'),
(8, 10, 'hjkhjhj');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `informationpagedesign`
--

DROP TABLE IF EXISTS `informationpagedesign`;
CREATE TABLE IF NOT EXISTS `informationpagedesign` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `fk_slider` bigint(20) NOT NULL,
  `fk_DesignType` bigint(20) NOT NULL,
  `image` varchar(100) DEFAULT NULL,
  `sound` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `fk_DesignType` (`fk_DesignType`),
  KEY `fk_slider` (`fk_slider`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `informationpagedesign`
--

INSERT INTO `informationpagedesign` (`id`, `fk_slider`, `fk_DesignType`, `image`, `sound`) VALUES
(11, 15, 7, 'error.png', 'ddf'),
(12, 16, 8, 'sdfsdf', 'sdfsdf');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `instancemodule`
--

DROP TABLE IF EXISTS `instancemodule`;
CREATE TABLE IF NOT EXISTS `instancemodule` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `fk_module` bigint(20) NOT NULL,
  `fk_supervisor` bigint(20) NOT NULL,
  `questionsNumber` int(11) NOT NULL,
  `time` varchar(20) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_module` (`fk_module`),
  KEY `fk_supervisor` (`fk_supervisor`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `instancemodule`
--

INSERT INTO `instancemodule` (`id`, `fk_module`, `fk_supervisor`, `questionsNumber`, `time`) VALUES
(1, 10, 1, 4, '10');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `module`
--

DROP TABLE IF EXISTS `module`;
CREATE TABLE IF NOT EXISTS `module` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `fk_moduleType` bigint(20) NOT NULL,
  `name` varchar(50) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `name` (`name`),
  KEY `fk_moduleType` (`fk_moduleType`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `module`
--

INSERT INTO `module` (`id`, `fk_moduleType`, `name`) VALUES
(10, 1, 'Prueba'),
(11, 1, 'Prueba2');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `moduletype`
--

DROP TABLE IF EXISTS `moduletype`;
CREATE TABLE IF NOT EXISTS `moduletype` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `moduletype`
--

INSERT INTO `moduletype` (`id`, `name`) VALUES
(1, 'Módulo de Información'),
(2, 'Módulo de Chequeo'),
(3, 'Módulo Operacional');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `operationalcheckquestion`
--

DROP TABLE IF EXISTS `operationalcheckquestion`;
CREATE TABLE IF NOT EXISTS `operationalcheckquestion` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `fk_ckeckModule` bigint(20) NOT NULL,
  `questionName` varchar(200) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_ckeckModule` (`fk_ckeckModule`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `operationalmodeulecheckdetail`
--

DROP TABLE IF EXISTS `operationalmodeulecheckdetail`;
CREATE TABLE IF NOT EXISTS `operationalmodeulecheckdetail` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `fk_operationalModuleDetail` bigint(20) NOT NULL,
  `QuestionMade` bigint(20) NOT NULL,
  `questionface` int(11) NOT NULL,
  `correctAnswer` tinyint(1) NOT NULL,
  `madeAnswer` tinyint(1) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_operationalModuleDetail` (`fk_operationalModuleDetail`),
  KEY `fk_questionMade` (`QuestionMade`),
  KEY `QuestionMade` (`QuestionMade`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `operationalmodule`
--

DROP TABLE IF EXISTS `operationalmodule`;
CREATE TABLE IF NOT EXISTS `operationalmodule` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `fk_moduleType` bigint(20) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_moduleType` (`fk_moduleType`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `operationalmoduleanswer`
--

DROP TABLE IF EXISTS `operationalmoduleanswer`;
CREATE TABLE IF NOT EXISTS `operationalmoduleanswer` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `text` text NOT NULL,
  `correct` tinyint(1) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `operationalmoduledetail`
--

DROP TABLE IF EXISTS `operationalmoduledetail`;
CREATE TABLE IF NOT EXISTS `operationalmoduledetail` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `fk_realizationModule` bigint(20) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_realizationModule` (`fk_realizationModule`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `operationalmoduleestadisticdetail`
--

DROP TABLE IF EXISTS `operationalmoduleestadisticdetail`;
CREATE TABLE IF NOT EXISTS `operationalmoduleestadisticdetail` (
  `id` int(11) NOT NULL,
  `fk_operationalModuleDetail` bigint(20) NOT NULL,
  `fk_Detail` bigint(20) NOT NULL,
  `value` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_operationalModuleDetail` (`fk_operationalModuleDetail`),
  KEY `fk_Detail` (`fk_Detail`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `operationalmodulequestiondetail`
--

DROP TABLE IF EXISTS `operationalmodulequestiondetail`;
CREATE TABLE IF NOT EXISTS `operationalmodulequestiondetail` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `fk_operationalModuleDetail` bigint(20) NOT NULL,
  `answerMade` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_operationalModuleDetail` (`fk_operationalModuleDetail`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `operationalmodulequestions`
--

DROP TABLE IF EXISTS `operationalmodulequestions`;
CREATE TABLE IF NOT EXISTS `operationalmodulequestions` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `fk_moduleInformationQuestion` bigint(20) NOT NULL,
  `question` text NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_moduleInformationQuestion` (`fk_moduleInformationQuestion`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `questionmakedinformationmodule`
--

DROP TABLE IF EXISTS `questionmakedinformationmodule`;
CREATE TABLE IF NOT EXISTS `questionmakedinformationmodule` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `fk_realizationModule` int(11) NOT NULL,
  `fk_questionID` int(11) NOT NULL,
  `fk_answerID` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=185 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `questionmakedinformationmodule`
--

INSERT INTO `questionmakedinformationmodule` (`id`, `fk_realizationModule`, `fk_questionID`, `fk_answerID`) VALUES
(1, 1, 6, 17),
(2, 1, 6, 13),
(3, 1, 6, 15),
(4, 1, 6, 11),
(5, 1, 7, 8),
(6, 1, 7, 6),
(7, 1, 7, 18),
(8, 1, 7, 19),
(9, 1, 6, 11),
(10, 1, 6, 16),
(11, 1, 6, 17),
(12, 1, 6, 13),
(13, 1, 7, 6),
(14, 1, 7, 19),
(15, 1, 7, 8),
(16, 1, 7, 7),
(17, 1, 6, 17),
(18, 1, 6, 16),
(19, 1, 6, 13),
(20, 1, 6, 11),
(21, 1, 7, 7),
(22, 1, 7, 8),
(23, 1, 7, 18),
(24, 1, 7, 6),
(25, 1, 6, 13),
(26, 1, 6, 17),
(27, 1, 6, 11),
(28, 1, 6, 16),
(29, 1, 7, 7),
(30, 1, 7, 8),
(31, 1, 7, 19),
(32, 1, 7, 6),
(33, 1, 6, 17),
(34, 1, 6, 11),
(35, 1, 6, 16),
(36, 1, 6, 15),
(37, 1, 7, 6),
(38, 1, 7, 19),
(39, 1, 7, 8),
(40, 1, 7, 7),
(41, 1, 6, 17),
(42, 1, 6, 13),
(43, 1, 6, 11),
(44, 1, 6, 16),
(45, 1, 7, 19),
(46, 1, 7, 6),
(47, 1, 7, 7),
(48, 1, 7, 8),
(49, 1, 6, 11),
(50, 1, 6, 17),
(51, 1, 6, 15),
(52, 1, 6, 14),
(53, 1, 7, 18),
(54, 1, 7, 7),
(55, 1, 7, 6),
(56, 1, 7, 8),
(57, 1, 6, 14),
(58, 1, 6, 11),
(59, 1, 6, 17),
(60, 1, 6, 15),
(61, 1, 7, 19),
(62, 1, 7, 6),
(63, 1, 7, 7),
(64, 1, 7, 8),
(65, 1, 6, 13),
(66, 1, 6, 14),
(67, 1, 6, 16),
(68, 1, 6, 11),
(69, 1, 7, 19),
(70, 1, 7, 8),
(71, 1, 7, 6),
(72, 1, 7, 7),
(73, 1, 6, 11),
(74, 1, 6, 14),
(75, 1, 6, 16),
(76, 1, 6, 15),
(77, 1, 7, 19),
(78, 1, 7, 8),
(79, 1, 7, 7),
(80, 1, 7, 18),
(81, 1, 6, 16),
(82, 1, 6, 14),
(83, 1, 6, 11),
(84, 1, 6, 17),
(85, 1, 7, 8),
(86, 1, 7, 6),
(87, 1, 7, 19),
(88, 1, 7, 7),
(89, 1, 6, 14),
(90, 1, 6, 16),
(91, 1, 6, 13),
(92, 1, 6, 11),
(93, 1, 7, 19),
(94, 1, 7, 8),
(95, 1, 7, 7),
(96, 1, 7, 18),
(97, 1, 6, 17),
(98, 1, 6, 14),
(99, 1, 6, 15),
(100, 1, 6, 11),
(101, 1, 7, 18),
(102, 1, 7, 8),
(103, 1, 7, 19),
(104, 1, 7, 7),
(105, 1, 6, 15),
(106, 1, 6, 17),
(107, 1, 6, 11),
(108, 1, 6, 16),
(109, 1, 7, 8),
(110, 1, 7, 6),
(111, 1, 7, 18),
(112, 1, 7, 19),
(113, 1, 6, 16),
(114, 1, 6, 17),
(115, 1, 6, 11),
(116, 1, 6, 15),
(117, 1, 7, 18),
(118, 1, 7, 8),
(119, 1, 7, 7),
(120, 1, 7, 19),
(121, 1, 6, 14),
(122, 1, 6, 11),
(123, 1, 6, 16),
(124, 1, 6, 15),
(125, 1, 7, 7),
(126, 1, 7, 6),
(127, 1, 7, 8),
(128, 1, 7, 19),
(129, 1, 6, 11),
(130, 1, 6, 17),
(131, 1, 6, 14),
(132, 1, 6, 13),
(133, 1, 7, 6),
(134, 1, 7, 8),
(135, 1, 7, 19),
(136, 1, 7, 18),
(137, 1, 6, 11),
(138, 1, 6, 14),
(139, 1, 6, 13),
(140, 1, 6, 15),
(141, 1, 7, 19),
(142, 1, 7, 6),
(143, 1, 7, 7),
(144, 1, 7, 8),
(145, 1, 6, 17),
(146, 1, 6, 11),
(147, 1, 6, 13),
(148, 1, 6, 14),
(149, 1, 7, 19),
(150, 1, 7, 8),
(151, 1, 7, 6),
(152, 1, 7, 7),
(153, 1, 6, 11),
(154, 1, 6, 17),
(155, 1, 6, 16),
(156, 1, 6, 15),
(157, 1, 7, 7),
(158, 1, 7, 18),
(159, 1, 7, 19),
(160, 1, 7, 8),
(161, 1, 6, 17),
(162, 1, 6, 16),
(163, 1, 6, 11),
(164, 1, 6, 15),
(165, 1, 7, 8),
(166, 1, 7, 19),
(167, 1, 7, 7),
(168, 1, 7, 18),
(169, 1, 8, 20),
(170, 1, 8, 21),
(171, 1, 8, 22),
(172, 1, 8, 0),
(173, 1, 6, 15),
(174, 1, 6, 11),
(175, 1, 6, 13),
(176, 1, 6, 17),
(177, 1, 7, 7),
(178, 1, 7, 19),
(179, 1, 7, 8),
(180, 1, 7, 18),
(181, 1, 8, 20),
(182, 1, 8, 21),
(183, 1, 8, 24),
(184, 1, 8, 22);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `realizationmodule`
--

DROP TABLE IF EXISTS `realizationmodule`;
CREATE TABLE IF NOT EXISTS `realizationmodule` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `fk_user` bigint(20) NOT NULL,
  `fk_moduleInstance` bigint(20) NOT NULL,
  `dateIni` varchar(20) NOT NULL,
  `dateEnd` varchar(20) NOT NULL,
  `supervisor` bigint(20) NOT NULL,
  `approvalPercent` int(11) NOT NULL,
  `obtainedPercent` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_user` (`fk_user`),
  KEY `fk_moduleInstance` (`fk_moduleInstance`),
  KEY `fk_user_2` (`fk_user`),
  KEY `fk_moduleInstance_2` (`fk_moduleInstance`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `realizationmodule`
--

INSERT INTO `realizationmodule` (`id`, `fk_user`, `fk_moduleInstance`, `dateIni`, `dateEnd`, `supervisor`, `approvalPercent`, `obtainedPercent`) VALUES
(1, 1, 1, '', '', 0, 0, 0),
(2, 1, 1, '12', '12', 1, 12, 12);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `slider`
--

DROP TABLE IF EXISTS `slider`;
CREATE TABLE IF NOT EXISTS `slider` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `fk_module` bigint(20) NOT NULL,
  `Title` varchar(100) NOT NULL,
  `orderSlider` int(11) NOT NULL,
  `designe` varchar(50) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_informationModule` (`fk_module`),
  KEY `fk_informationModule_2` (`fk_module`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `slider`
--

INSERT INTO `slider` (`id`, `fk_module`, `Title`, `orderSlider`, `designe`) VALUES
(11, 10, 'Informacion primaria', 0, 'Diseño1.png'),
(15, 10, 'aa', 1, 'Diseño2.png'),
(16, 10, 'sdf', 2, 'Diseño3.png');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `statistics`
--

DROP TABLE IF EXISTS `statistics`;
CREATE TABLE IF NOT EXISTS `statistics` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `fk_OperationalModule` bigint(20) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_OperationalModule` (`fk_OperationalModule`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `statisticsdetail`
--

DROP TABLE IF EXISTS `statisticsdetail`;
CREATE TABLE IF NOT EXISTS `statisticsdetail` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `fk_statistics` bigint(20) NOT NULL,
  `fk_detail` bigint(20) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_statistics` (`fk_statistics`),
  KEY `fk_detail` (`fk_detail`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `structure`
--

DROP TABLE IF EXISTS `structure`;
CREATE TABLE IF NOT EXISTS `structure` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `fk_informationPageDesign` bigint(20) NOT NULL,
  `text` text NOT NULL,
  `icon` varchar(50) NOT NULL,
  `background` varchar(50) NOT NULL,
  `fontColor` varchar(20) NOT NULL,
  `orderStructure` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_informationPageDesign` (`fk_informationPageDesign`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `structure`
--

INSERT INTO `structure` (`id`, `fk_informationPageDesign`, `text`, `icon`, `background`, `fontColor`, `orderStructure`) VALUES
(3, 11, 'sdfsdf', '1a.png', '', 'Black ', 0),
(5, 12, 'sdfsdf', '1a.png', 'White', 'Black  ', 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `supervisor`
--

DROP TABLE IF EXISTS `supervisor`;
CREATE TABLE IF NOT EXISTS `supervisor` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `user` varchar(50) NOT NULL,
  `pass` varchar(50) NOT NULL,
  `name` varchar(50) DEFAULT NULL,
  `lastName` varchar(50) DEFAULT NULL,
  `rut` varchar(50) DEFAULT NULL,
  `address` varchar(100) DEFAULT NULL,
  `email` varchar(50) DEFAULT NULL,
  `phone` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `supervisor`
--

INSERT INTO `supervisor` (`id`, `user`, `pass`, `name`, `lastName`, `rut`, `address`, `email`, `phone`) VALUES
(1, 'supervisor1', 'sup1', 'Mike', 'Launher', '15785495-5', 'direccion 1', 'email1', '586558565');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `user`
--

DROP TABLE IF EXISTS `user`;
CREATE TABLE IF NOT EXISTS `user` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `user` varchar(50) NOT NULL,
  `pass` varchar(50) NOT NULL,
  `fk_supervisor` bigint(20) NOT NULL,
  `name` varchar(50) DEFAULT NULL,
  `lastname` varchar(50) DEFAULT NULL,
  `rut` varchar(20) DEFAULT NULL,
  `address` varchar(100) DEFAULT NULL,
  `email` varchar(50) DEFAULT NULL,
  `phone` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_supervisor` (`fk_supervisor`),
  KEY `fk_supervisor_2` (`fk_supervisor`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `user`
--

INSERT INTO `user` (`id`, `user`, `pass`, `fk_supervisor`, `name`, `lastname`, `rut`, `address`, `email`, `phone`) VALUES
(1, 'user1', 'us1', 1, NULL, NULL, NULL, NULL, NULL, NULL);

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `checkmodule`
--
ALTER TABLE `checkmodule`
  ADD CONSTRAINT `CheckModule_ibfk_1` FOREIGN KEY (`fk_moduleType`) REFERENCES `moduletype` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `checkmoduledetail`
--
ALTER TABLE `checkmoduledetail`
  ADD CONSTRAINT `CheckModuleDetail_ibfk_1` FOREIGN KEY (`fk_realizationModule`) REFERENCES `realizationmodule` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `CheckModuleDetail_ibfk_2` FOREIGN KEY (`fk_checkQuestion`) REFERENCES `checkquestions` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `checkquestions`
--
ALTER TABLE `checkquestions`
  ADD CONSTRAINT `CheckQuestions_ibfk_1` FOREIGN KEY (`fk_ckeckModule`) REFERENCES `checkmodule` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `informationmoduleanswers`
--
ALTER TABLE `informationmoduleanswers`
  ADD CONSTRAINT `InformationModuleAnswers_ibfk_1` FOREIGN KEY (`fk_informationModuleQuestions`) REFERENCES `informationmodulequestion` (`id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  ADD CONSTRAINT `InformationModuleAnswers_ibfk_2` FOREIGN KEY (`fk_informationModuleQuestions`) REFERENCES `informationmodulequestion` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `informationmoduledetail`
--
ALTER TABLE `informationmoduledetail`
  ADD CONSTRAINT `InformationModuleDetail_ibfk_1` FOREIGN KEY (`fk_informationModuleAnswers`) REFERENCES `informationmoduleanswers` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `InformationModuleDetail_ibfk_2` FOREIGN KEY (`fk_realizationModule`) REFERENCES `realizationmodule` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `informationmodulequestion`
--
ALTER TABLE `informationmodulequestion`
  ADD CONSTRAINT `InformationModuleQuestion_ibfk_1` FOREIGN KEY (`fk_module`) REFERENCES `module` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `informationpagedesign`
--
ALTER TABLE `informationpagedesign`
  ADD CONSTRAINT `InformationPageDesign_ibfk_1` FOREIGN KEY (`fk_slider`) REFERENCES `slider` (`id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  ADD CONSTRAINT `InformationPageDesign_ibfk_2` FOREIGN KEY (`fk_DesignType`) REFERENCES `designtype` (`id`) ON DELETE NO ACTION ON UPDATE CASCADE;

--
-- Filtros para la tabla `instancemodule`
--
ALTER TABLE `instancemodule`
  ADD CONSTRAINT `InstanceModule_ibfk_1` FOREIGN KEY (`fk_module`) REFERENCES `module` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `InstanceModule_ibfk_2` FOREIGN KEY (`fk_supervisor`) REFERENCES `supervisor` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `module`
--
ALTER TABLE `module`
  ADD CONSTRAINT `Module_ibfk_1` FOREIGN KEY (`fk_moduleType`) REFERENCES `moduletype` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `operationalcheckquestion`
--
ALTER TABLE `operationalcheckquestion`
  ADD CONSTRAINT `OperationalCheckQuestion_ibfk_1` FOREIGN KEY (`fk_ckeckModule`) REFERENCES `checkmodule` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `operationalmodeulecheckdetail`
--
ALTER TABLE `operationalmodeulecheckdetail`
  ADD CONSTRAINT `OperationalModeuleCheckDetail_ibfk_1` FOREIGN KEY (`fk_operationalModuleDetail`) REFERENCES `operationalmoduledetail` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `operationalmodule`
--
ALTER TABLE `operationalmodule`
  ADD CONSTRAINT `OperationalModule_ibfk_1` FOREIGN KEY (`fk_moduleType`) REFERENCES `moduletype` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `operationalmoduledetail`
--
ALTER TABLE `operationalmoduledetail`
  ADD CONSTRAINT `OperationalModuleDetail_ibfk_1` FOREIGN KEY (`fk_realizationModule`) REFERENCES `realizationmodule` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `operationalmoduleestadisticdetail`
--
ALTER TABLE `operationalmoduleestadisticdetail`
  ADD CONSTRAINT `OperationalModuleEstadisticDetail_ibfk_1` FOREIGN KEY (`fk_operationalModuleDetail`) REFERENCES `operationalmoduledetail` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `OperationalModuleEstadisticDetail_ibfk_2` FOREIGN KEY (`fk_Detail`) REFERENCES `detail` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `operationalmodulequestiondetail`
--
ALTER TABLE `operationalmodulequestiondetail`
  ADD CONSTRAINT `OperationalModuleQuestionDetail_ibfk_1` FOREIGN KEY (`fk_operationalModuleDetail`) REFERENCES `operationalmoduledetail` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `operationalmodulequestions`
--
ALTER TABLE `operationalmodulequestions`
  ADD CONSTRAINT `OperationalModuleQuestions_ibfk_1` FOREIGN KEY (`fk_moduleInformationQuestion`) REFERENCES `informationmodulequestion` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `realizationmodule`
--
ALTER TABLE `realizationmodule`
  ADD CONSTRAINT `RealizationModule_ibfk_1` FOREIGN KEY (`fk_user`) REFERENCES `user` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `RealizationModule_ibfk_2` FOREIGN KEY (`fk_moduleInstance`) REFERENCES `instancemodule` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `slider`
--
ALTER TABLE `slider`
  ADD CONSTRAINT `Slider_ibfk_1` FOREIGN KEY (`fk_module`) REFERENCES `module` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `statistics`
--
ALTER TABLE `statistics`
  ADD CONSTRAINT `Statistics_ibfk_1` FOREIGN KEY (`fk_OperationalModule`) REFERENCES `operationalmodule` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `statisticsdetail`
--
ALTER TABLE `statisticsdetail`
  ADD CONSTRAINT `StatisticsDetail_ibfk_1` FOREIGN KEY (`fk_statistics`) REFERENCES `statistics` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `StatisticsDetail_ibfk_2` FOREIGN KEY (`fk_detail`) REFERENCES `detail` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `structure`
--
ALTER TABLE `structure`
  ADD CONSTRAINT `Structure_ibfk_1` FOREIGN KEY (`fk_informationPageDesign`) REFERENCES `informationpagedesign` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
