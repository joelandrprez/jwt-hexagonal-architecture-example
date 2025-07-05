INSERT INTO Rol (Id, Name, Description, CreatedAt, CreatedBy)
VALUES 
(1, 'Admin', 'Administrador del sistema', GETDATE(), 'system'),
(2, 'Student', 'Usuario estudiante', GETDATE(), 'system');


INSERT INTO Entity (Name, Description, CreatedAt, CreatedBy)
VALUES 
('Simulador', 'Entidad principal del sistema', GETDATE(), 'system');


INSERT INTO Course (Name, Description, EntityId, QuestionCount, TimeMinute, CreatedAt, CreatedBy)
VALUES 
('Curso PMP', 'Curso de preparación para PMP', 1, 100, 120, GETDATE(), 'admin');


INSERT INTO Topic (Name, Description, CourseId, CreatedAt, CreatedBy)
VALUES 
('Inicio del proyecto', 'Primera fase del proyecto', 1, GETDATE(), 'admin');


INSERT INTO Issue (Name, Description, TopicId, CreatedAt, CreatedBy)
VALUES 
('Acta de constitución', 'Documento clave del inicio del proyecto', 1, GETDATE(), 'admin');

INSERT INTO StatusSubscription (Id, Name, Description, CreatedAt, CreatedBy)
VALUES 
(1, 'Activa', 'Suscripción en estado activo', GETDATE(), 'system'),
(2, 'Inactiva', 'Suscripción expirada o cancelada', GETDATE(), 'system');

INSERT INTO ExamMode (Id, Name, Description, CreatedAt, CreatedBy)
VALUES 
(1, 'Práctica', 'Modo de práctica sin tiempo límite', GETDATE(), 'admin'),
(2, 'Simulación', 'Simulación del examen real con tiempo límite', GETDATE(), 'admin');

INSERT INTO TypeExam (Id, Name, Description, CreatedAt, CreatedBy)
VALUES 
(1, 'Evaluación Final', 'Examen final del curso', GETDATE(), 'admin'),
(2, 'Autoevaluación', 'Examen de autoevaluación al finalizar el tema', GETDATE(), 'admin');


INSERT INTO Subscription (Name, Description, ExpirationDate, SubscriptionDate, CourseId, StatusId, UserId, CreatedAt, CreatedBy)
VALUES 
('Suscripción PMP Básico', 'Acceso al curso PMP', '2025-12-31', GETDATE(), 1, 1, '3fa85f64-5717-4562-b3fc-2c963f66afa6', GETDATE(), 'admin');
