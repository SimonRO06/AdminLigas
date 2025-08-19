```sql
CREATE DATABASE TorneosDB;
USE TorneosDB;

CREATE TABLE Torneo (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    tipo VARCHAR(50) NOT NULL,
    fecha_inicio DATE NOT NULL,
    fecha_fin DATE NOT NULL
);

CREATE TABLE Equipo (
    id INT AUTO_INCREMENT PRIMARY KEY, 
    nombre VARCHAR(100) NOT NULL,
    tipo VARCHAR(50) NOT NULL,
    pais VARCHAR(50) NOT NULL,
    goles_contra INT NOT NULL DEFAULT 0,
    dinero DECIMAL(15,2) NOT NULL DEFAULT 0
);

CREATE TABLE Jugador (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    apellido VARCHAR(100) NOT NULL,
    edad INT NOT NULL,
    dorsal INT NOT NULL,
    posicion VARCHAR(50) NOT NULL,
    precio DECIMAL(10,2) DEFAULT 0,
    asistencias INT NOT NULL DEFAULT 0,
    equipoId INT,
    FOREIGN KEY (equipoId) REFERENCES Equipo(id)
);

CREATE TABLE CuerpoTecnico (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    apellido VARCHAR(100) NOT NULL,
    edad INT NOT NULL,
    cargo VARCHAR(100) NOT NULL,
    equipoId INT NOT NULL,
    FOREIGN KEY (equipoId) REFERENCES Equipo(id)
);

CREATE TABLE CuerpoMedico (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    apellido VARCHAR(100) NOT NULL,
    edad INT NOT NULL,
    especialidad VARCHAR(100) NOT NULL,
    equipoId INT NOT NULL,
    FOREIGN KEY (equipoId) REFERENCES Equipo(id)
);

CREATE TABLE Torneo_Equipo (
    torneoId INT NOT NULL,
    equipoId INT NOT NULL,
    PRIMARY KEY (torneoId, equipoId),
    FOREIGN KEY (torneoId) REFERENCES Torneo(id),
    FOREIGN KEY (equipoId) REFERENCES Equipo(id)
);

CREATE TABLE Transferencia (
    id INT AUTO_INCREMENT PRIMARY KEY,
    playerId INT NOT NULL,
    fromTeamId INT NOT NULL,
    toTeamId INT NOT NULL,
    amount DECIMAL(10,2) NOT NULL DEFAULT 0,
    type VARCHAR(50) NOT NULL,
    date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (playerId) REFERENCES Jugador(id) ON DELETE CASCADE,
    FOREIGN KEY (fromTeamId) REFERENCES Equipo(id) ON DELETE CASCADE,
    FOREIGN KEY (toTeamId) REFERENCES Equipo(id) ON DELETE CASCADE
);
```