-- phpMyAdmin SQL Dump
-- version 5.0.4
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 24-11-2021 a las 22:31:41
-- Versión del servidor: 10.4.16-MariaDB
-- Versión de PHP: 7.4.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `droplearn2.0`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `cursos`
--

CREATE TABLE `cursos` (
  `idCursos` int(11) NOT NULL,
  `Clave` int(11) NOT NULL,
  `Nombre` varchar(45) NOT NULL,
  `Profesor` varchar(45) NOT NULL,
  `Escuela` varchar(11) DEFAULT NULL,
  `cant_personas` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `cursos`
--

INSERT INTO `cursos` (`idCursos`, `Clave`, `Nombre`, `Profesor`, `Escuela`, `cant_personas`) VALUES
(1, 26273, 'Automatas', 'Antonio', 'UDG', 6),
(2, 63723, 'Inteligencia', 'Ismael', 'TECMM', 21),
(3, 32737, 'Ensamblador', 'Miguel Bernan', 'TECMM Zapo', 10),
(4, 45456, 'Cultura y belleza', 'Alejandra', 'TECMM', 4),
(5, 32623, 'Arquitectura', 'Leon Ramirez', 'UDG', 20),
(6, 45487, 'Astronomia', 'Carl Sagan', 'UTEG', 4),
(8, 63732, 'React', 'Hugo', 'Udemy', 16),
(16, 36747, 'Javascript Web', 'Fernando', 'Udemy', 41);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `cu_est`
--

CREATE TABLE `cu_est` (
  `idCu_Est` int(11) NOT NULL,
  `idCursos` int(11) NOT NULL,
  `idPerfil` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `cu_est`
--

INSERT INTO `cu_est` (`idCu_Est`, `idCursos`, `idPerfil`) VALUES
(8, 3, 10),
(10, 8, 10),
(11, 4, 10),
(12, 2, 4),
(13, 1, 4),
(14, 6, 1),
(15, 8, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `cu_pro`
--

CREATE TABLE `cu_pro` (
  `idCu_Pro` int(11) NOT NULL,
  `idPerfil` int(11) NOT NULL,
  `idCursos` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `cu_pro`
--

INSERT INTO `cu_pro` (`idCu_Pro`, `idPerfil`, `idCursos`) VALUES
(1, 4, 1),
(2, 4, 2);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `evaluaciones`
--

CREATE TABLE `evaluaciones` (
  `idEvaluaciones` int(11) NOT NULL,
  `NombreEstudiante` varchar(45) NOT NULL,
  `TotaldeActividades` int(11) NOT NULL,
  `Promedio` int(11) NOT NULL,
  `Certificaciones` int(11) DEFAULT NULL,
  `Progreso` int(11) DEFAULT NULL,
  `idEstudiantes` int(11) NOT NULL,
  `idCursos` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `evaluaciones`
--

INSERT INTO `evaluaciones` (`idEvaluaciones`, `NombreEstudiante`, `TotaldeActividades`, `Promedio`, `Certificaciones`, `Progreso`, `idEstudiantes`, `idCursos`) VALUES
(1, 'Jaime Armando', 21, 86, NULL, NULL, 0, 1),
(2, 'Jaime Armando', 16, 96, 1, 100, 0, 3),
(3, 'Jaime Armando', 25, 90, 1, NULL, 0, 1),
(4, 'Osvaldo ', 14, 84, NULL, NULL, 0, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `perfil`
--

CREATE TABLE `perfil` (
  `idPerfil` int(11) NOT NULL,
  `imagen` longblob DEFAULT NULL,
  `Cursos` int(11) NOT NULL,
  `Correo` varchar(45) NOT NULL,
  `Escolaridad` varchar(45) DEFAULT NULL,
  `NiveldeEstudios` varchar(45) NOT NULL,
  `edad` int(11) NOT NULL,
  `idRegistro` int(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `perfil`
--

INSERT INTO `perfil` (`idPerfil`, `imagen`, `Cursos`, `Correo`, `Escolaridad`, `NiveldeEstudios`, `edad`, `idRegistro`) VALUES
(1, NULL, 6, 'jaimesitomaton@88gmail.com', 'TECMM', 'Bachillerato', 18, 1),
(2, NULL, 9, 'ismaelRBJ@hotmail.com', NULL, 'ING.Sistemas Computacionales', 34, 4),
(3, NULL, 4, 'OsvaldoMarciano99@hotmail.com', 'TECMM', 'Bachillerato', 20, 2),
(4, NULL, 12, 'RobertoVenegas@zapopan.tecmm.edu.mx', 'TECMM', 'ING.Sistemas Computacionales', 38, 3),
(10, NULL, 6, 'mari@gmail.com', 'Centro Universitario Azteca', 'Bachillerato', 22, 7),
(17, NULL, 8, 'armando@cv.com', 'CETI Colomos', 'Ingenieria', 48, 9),
(18, NULL, 0, 'cara@hotmail.com', 'TECMM', 'Bachillerato', 20, 25),
(25, NULL, 0, 'yoli@hotmail.com', 'TECMM', 'Bachillerato', 22, 27),
(32, NULL, 0, 'lalas@gmail.com', 'UTEG', 'lalas', 22, 26),
(36, NULL, 0, 'selen@gmail.com', 'UTEG', 'Bachillerato', 24, 34);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `registro`
--

CREATE TABLE `registro` (
  `idRegistro` int(20) NOT NULL,
  `Nombre` varchar(45) NOT NULL,
  `Correo` varchar(45) NOT NULL,
  `TipodeUsuario` varchar(45) DEFAULT NULL,
  `Contraseña` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `registro`
--

INSERT INTO `registro` (`idRegistro`, `Nombre`, `Correo`, `TipodeUsuario`, `Contraseña`) VALUES
(1, 'Jaime Armando', 'jaimesitomaton@88gmail.com', 'Estudiante', '23423'),
(2, 'Osvaldo Mazas', 'OsvaldoMarciano99@hotmail.com', 'Estudiante', '643534'),
(3, 'Roberto Venegas', 'RobertoVenegas@zapopan.tecmm.edu.mx', 'Profesor', '45345'),
(4, 'Ismal Ascencio', 'IsmaelRBJ@hotmail.com', 'Profesor', '5123213'),
(6, 'Juan', 'juanga@hotmail.com', 'Profesor', '53232'),
(7, 'maria', 'mari@gmail.com', 'Estudiante', '1234'),
(9, 'Armando Comparan', 'armando@cv.com', 'Teacher', '1234567'),
(25, 'Caramelo', 'cara@hotmail.com', 'Student', '123'),
(26, 'lalas', 'lalas@gmail.com', 'Estudiante', '13131'),
(27, 'Yolanda', 'yoli@hotmail.com', 'Estudiante', '12345'),
(29, 'Esmeralda', 'esme@hotmail.com', 'Estudiante', '133223'),
(34, 'Selena', 'selen@gmail.com', 'Estudiante', '123'),
(50, 'romando', 'roman99@outlook.com', 'estudiante', '1324c2ad4'),
(51, 'venegas', 'venegas99@gmail.com', 'profesor', '2414f3254'),
(52, 'Erick Larios', 'ErickProgra88@hotmail.com', 'estudiante', '21232d2d2'),
(53, 'alberto', 'alberto@hotmail.com', 'estudiante', '32dweadawdwd'),
(56, 'marcelo', 'charmerlito99@outlook.com', 'Student', '12345678'),
(57, 'juanpa zurita', 'zuritagonza105@hotmail.com', 'Teacher', '87654321');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tablon`
--

CREATE TABLE `tablon` (
  `idTablon` int(11) NOT NULL,
  `Publicacion` varchar(45) NOT NULL,
  `FechaProxima` date NOT NULL,
  `Comentarios` varchar(350) DEFAULT NULL,
  `idCursos` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `tablon`
--

INSERT INTO `tablon` (`idTablon`, `Publicacion`, `FechaProxima`, `Comentarios`, `idCursos`) VALUES
(0, 'Bellas Artes', '2021-01-14', 'Crea un cuadro de pintura con crayola', 4),
(1, 'Material sobre proyecto parte 2', '2020-11-17', '4', 1),
(2, 'Practica 3 Paradigma de la programacion', '2020-11-19', '14', 2),
(3, 'Ejercicio 2 Dimensiones Cuanticas', '2020-11-20', NULL, 2),
(5, 'Materia cuantica', '2021-01-22', 'Debes investigar la constitucion de un atomo', 8),
(6, 'Mapa Conceptual Big Bang', '2021-01-20', 'No se les olvide consultar la informacion proporcionada en  el tablon', 6),
(7, 'Marco conceptual de Nikola Tesla', '2021-02-12', 'Investiguen sus antecedentes cientificos y proporcionenlos en un marco', 8),
(9, 'Presentacion ppt de la relatividad del tiempo', '2021-01-28', 'Analicen la informacion que se les proporciono en los documentos del curso', 8),
(10, 'Modelaje', '2020-12-17', 'Traigan sus vestidos mas bonitos', 4),
(11, 'Orden al manipular cubiertos', '2020-12-25', 'Hagan un video sobre como manipulan los cubiertos', 4),
(12, 'Cortes y maquillaje', '2020-12-16', NULL, 4),
(13, 'Dibujos del sistema solar', '2020-12-31', 'Representen su dibujo en una maqueta y suban la imagen', 6),
(14, 'PHP', '2020-12-16', 'Creen un documento php en el programa que deseen puede ser sublime o notepad', 1),
(15, 'HTML y CSS', '2020-12-31', 'Hagan un documento en sublime o notepad', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `trabajos`
--

CREATE TABLE `trabajos` (
  `idTrabajo` int(11) NOT NULL,
  `Nombre` varchar(45) NOT NULL,
  `Descripcion` varchar(45) DEFAULT NULL,
  `FechaCreacion` date NOT NULL,
  `FechaEntrega` date NOT NULL,
  `Documentos` varchar(45) DEFAULT NULL,
  `idTablon` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `trabajos`
--

INSERT INTO `trabajos` (`idTrabajo`, `Nombre`, `Descripcion`, `FechaCreacion`, `FechaEntrega`, `Documentos`, `idTablon`) VALUES
(1, 'Paradigma de la programacion', 'lee el documento', '2020-11-15', '2020-11-18', 'PDF', 2),
(2, 'Dimensiones Cuanticas', 'Calcular  las 3 dimensiones', '2020-11-12', '2020-11-15', 'WORD', 3),
(3, 'Trabajo de materia cuantica', 'Referenci con Fisica Cuantica', '2020-12-25', '2020-12-26', 'PDF', 5);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `cursos`
--
ALTER TABLE `cursos`
  ADD PRIMARY KEY (`idCursos`),
  ADD KEY `Estudiantes_ID` (`cant_personas`);

--
-- Indices de la tabla `cu_est`
--
ALTER TABLE `cu_est`
  ADD PRIMARY KEY (`idCu_Est`),
  ADD KEY `idCu_Est` (`idCu_Est`),
  ADD KEY `Nombre` (`idCursos`),
  ADD KEY `idPerfil` (`idPerfil`);

--
-- Indices de la tabla `cu_pro`
--
ALTER TABLE `cu_pro`
  ADD PRIMARY KEY (`idCu_Pro`),
  ADD KEY `idCursos` (`idCursos`),
  ADD KEY `idPerfil` (`idPerfil`) USING BTREE;

--
-- Indices de la tabla `evaluaciones`
--
ALTER TABLE `evaluaciones`
  ADD PRIMARY KEY (`idEvaluaciones`),
  ADD KEY `Curso_ID` (`idCursos`),
  ADD KEY `evaluaciones_ibfk_1` (`idEstudiantes`);

--
-- Indices de la tabla `perfil`
--
ALTER TABLE `perfil`
  ADD PRIMARY KEY (`idPerfil`),
  ADD KEY `RegistroID` (`idRegistro`);

--
-- Indices de la tabla `registro`
--
ALTER TABLE `registro`
  ADD PRIMARY KEY (`idRegistro`);

--
-- Indices de la tabla `tablon`
--
ALTER TABLE `tablon`
  ADD PRIMARY KEY (`idTablon`),
  ADD KEY `Curso_ID` (`idCursos`);

--
-- Indices de la tabla `trabajos`
--
ALTER TABLE `trabajos`
  ADD PRIMARY KEY (`idTrabajo`),
  ADD KEY `Tablon_ID` (`idTablon`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `cursos`
--
ALTER TABLE `cursos`
  MODIFY `idCursos` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT de la tabla `cu_est`
--
ALTER TABLE `cu_est`
  MODIFY `idCu_Est` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- AUTO_INCREMENT de la tabla `cu_pro`
--
ALTER TABLE `cu_pro`
  MODIFY `idCu_Pro` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `evaluaciones`
--
ALTER TABLE `evaluaciones`
  MODIFY `idEvaluaciones` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `perfil`
--
ALTER TABLE `perfil`
  MODIFY `idPerfil` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=37;

--
-- AUTO_INCREMENT de la tabla `registro`
--
ALTER TABLE `registro`
  MODIFY `idRegistro` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=58;

--
-- AUTO_INCREMENT de la tabla `trabajos`
--
ALTER TABLE `trabajos`
  MODIFY `idTrabajo` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `cu_est`
--
ALTER TABLE `cu_est`
  ADD CONSTRAINT `cu_est_ibfk_1` FOREIGN KEY (`idPerfil`) REFERENCES `perfil` (`idPerfil`) ON DELETE NO ACTION ON UPDATE CASCADE,
  ADD CONSTRAINT `cu_est_ibfk_2` FOREIGN KEY (`idCursos`) REFERENCES `cursos` (`idCursos`) ON DELETE NO ACTION ON UPDATE CASCADE;

--
-- Filtros para la tabla `cu_pro`
--
ALTER TABLE `cu_pro`
  ADD CONSTRAINT `cu_pro_ibfk_1` FOREIGN KEY (`idPerfil`) REFERENCES `perfil` (`idPerfil`) ON DELETE NO ACTION ON UPDATE CASCADE,
  ADD CONSTRAINT `cu_pro_ibfk_2` FOREIGN KEY (`idCursos`) REFERENCES `cursos` (`idCursos`) ON DELETE NO ACTION ON UPDATE CASCADE;

--
-- Filtros para la tabla `evaluaciones`
--
ALTER TABLE `evaluaciones`
  ADD CONSTRAINT `evaluaciones_ibfk_1` FOREIGN KEY (`idEstudiantes`) REFERENCES `estudiantes` (`idEstudiantes`) ON UPDATE CASCADE,
  ADD CONSTRAINT `evaluaciones_ibfk_2` FOREIGN KEY (`idCursos`) REFERENCES `cursos` (`idCursos`) ON UPDATE CASCADE;

--
-- Filtros para la tabla `perfil`
--
ALTER TABLE `perfil`
  ADD CONSTRAINT `perfil_ibfk_1` FOREIGN KEY (`idRegistro`) REFERENCES `registro` (`idRegistro`) ON UPDATE CASCADE;

--
-- Filtros para la tabla `tablon`
--
ALTER TABLE `tablon`
  ADD CONSTRAINT `tablon_ibfk_1` FOREIGN KEY (`idCursos`) REFERENCES `cursos` (`idCursos`) ON UPDATE CASCADE;

--
-- Filtros para la tabla `trabajos`
--
ALTER TABLE `trabajos`
  ADD CONSTRAINT `trabajos_ibfk_1` FOREIGN KEY (`idTablon`) REFERENCES `tablon` (`idTablon`) ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
