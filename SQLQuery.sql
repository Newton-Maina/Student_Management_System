IF OBJECT_ID('users', 'U') IS NOT NULL
DROP TABLE users;

IF OBJECT_ID('students', 'U') IS NOT NULL
DROP TABLE students;

IF OBJECT_ID('courses', 'U') IS NOT NULL
DROP TABLE courses;

CREATE TABLE users (
    id INT PRIMARY KEY IDENTITY (1,1),
    username VARCHAR(50) UNIQUE NOT NULL,
    password VARCHAR(50) NOT NULL
);

INSERT users (username, password) VALUES ('admin', 'admin123')
SELECT * FROM users

CREATE TABLE courses (
    course_id INT PRIMARY KEY IDENTITY(1,1),
    course_name VARCHAR(255) NOT NULL UNIQUE
);

INSERT INTO courses (course_name) 
VALUES ('BSc. Computer Science'), ('Bsc. Software Engineering'), ('Bsc. Information Technology');


CREATE TABLE students (
    id INT PRIMARY KEY IDENTITY(1,1),
    student_id VARCHAR(50) NOT NULL UNIQUE,         
    student_name VARCHAR(255) NOT NULL,
    student_gender VARCHAR(10) CHECK (student_gender IN ('Male', 'Female', 'Other')),
    student_address VARCHAR(255) NULL,
    student_grade VARCHAR(10) CHECK (student_grade IN ('A', 'A-', 'B+', 'B', 'B-', 'C+', 'C', 'C-', 'D+', 'D', 'D-', 'E')) NULL,   
    student_image VARCHAR(MAX) NULL,
    student_status VARCHAR(20) CHECK (student_status IN ('Enrolled', 'Inactive', 'Graduated')),
    course_name VARCHAR(MAX) NOT NULL,
    date_insert DATE NULL,
    date_update DATE NULL,
    date_delete DATE NULL,
);

SELECT * FROM students
SELECT * FROM courses