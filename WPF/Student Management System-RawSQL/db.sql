CREATE DATABASE flex;
USE flex;
drop TABLE student_data;
CREATE TABLE student_data
(
    id INT IDENTITY(1001,1) PRIMARY KEY,
    marks INT NOT NULL CHECK(marks>=0 AND marks<=100),
    student_name VARCHAR(50) NOT NULL,
    student_address VARCHAR(100),
    course VARCHAR(20),
    dept VARCHAR(10),
    campus VARCHAR(30)
);
SELECT * FROM student_data;
CREATE TABLE depts
(
dept_name VARCHAR(10)
);
-- ==============================================
CREATE TABLE campus
(
campus_code VARCHAR(5),
campus_name VARCHAR(20)
);
SELECT * FROM campus;
INSERT INTO depts (dept_name)
VALUES
('BS(CS)'),
('BS(AI)'),
('BS(DS)'),
('BS(SE)'),
('BS(CY)');
