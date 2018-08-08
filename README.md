# LSC - Controle de Vendas de Gado

# Objetivo
Esta aplicação foi desenvolvida para ser submetiva a avaliação do teste da K2, conforme arquivo PDF constante na raiz do projeto.

O prazo para o desenvolvimento foi de 3 dias, contudo por conta de um compromisso não pude usar os 3 dias, apenas 2.
Desta forma o mesmo foi desenvolvimento com o objetivo de atender o maior número de requisitos, tomando como princípio os que apresentavam ser de maior prioridad.
Todavia durante o trabalho alguns percalços ocorreu e assim a entrega se faz com ciência dos seguintes pontos

1. O tratamento de exception deve ser mais apurado.
2. Os mecanismos de modo geral estão rudimentares, uma solução mais robusta poderia ser implementada
3. O requisito "As compras impressas não podem ser alteradas ou excluídas" Não pode ser implementado por questão do tempo, como mencionado acima.

Para rodar a aplicação é necessário uma instância do SQL Server com um usuário válido e a base criada.
execute os seguintes comandos, substituindo para seu usuário e senha, para criação do usuário e base de dados

## Criação de Usuário na base

```
-- Criar login no servidor:
CREATE LOGIN YourUser WITH PASSWORD = 'YourStrongPassword';

-- Criar Base de dados
CREATE DATABASE lsc;
ALTER DATABASE lsc COLLATE Latin1_General_CI_AI;

-- Habilitar snapshot no banco
use master;
ALTER DATABASE lsc SET SINGLE_USER;
ALTER DATABASE lsc SET ALLOW_SNAPSHOT_ISOLATION ON;
ALTER DATABASE lsc SET READ_COMMITTED_SNAPSHOT ON;
ALTER DATABASE lsc SET MULTI_USER;

-- Criar  usuário no banco
use lsc;
CREATE USER YourUser FOR LOGIN YourUser WITH DEFAULT_SCHEMA=dbo;
ALTER AUTHORIZATION ON SCHEMA::[db_owner] TO YourUser;
ALTER ROLE db_owner ADD MEMBER YourUser

```

Editar o arquivo de configuração da aplicação WebApi mudando a string de conexão conforme padrao abaixo, lembrando de substituir os dados de host, user e password
para os valores que você utilizou.

"Server=YourHost\YourInstrance;initial catalog=lsc;persist security info=True;user id=YourUser;password=YourStrongPassword;application name=LSCApplication;MultipleActiveResultSets=True"

IMPORTANTE: Não existe um script para criação do banco, pois a implementação foi feita utilizando Fluent Api, assim, a própria aplicação irá criar o banco na primeira execução.