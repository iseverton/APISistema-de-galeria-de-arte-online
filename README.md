# 🖼️ Sistema de Galeria de Arte Online

O sistema será uma **plataforma de divulgação** para artistas, onde eles podem **exibir suas obras** e **entrar em contato** com os interessados. O objetivo principal não é realizar transações financeiras, mas fornecer a **visibilidade necessária** para que os **artistas** e **usuários** possam interagir diretamente.

## Requisitos

### **Cadastro e Autenticação de Usuários**

- O sistema deve permitir que os usuários se registrem utilizando:
    - E-mail
    - Nome
    - Senha
    - Telefone (opcional apenas para User)
    - Papel (user ou artista)

- **Login**
    - E-mail
    - Senha

- **Acesso baseado em roles:**
    - Com base nos papéis atribuídos ao usuário autenticado, uma série de funcionalidades estarão liberadas ou ocultas.
    - Os papéis são:
        - **user** (usuário comum)
        - **artista**
        - **moderador**
        - **admin**

### **Autorizações**

- **Funcionalidades progressivas:**
    - Se uma ação pode ser realizada por um **user**, ela também pode ser realizada por **artista**, **moderador** e **admin**.

- As seguintes funcionalidades estarão atreladas aos diferentes papéis:

    - **Usuário comum (user):**
        - Buscar artistas,obras e categorias.

    - **Artista:**
        - Cadastrar e gerenciar suas obras de arte.

    - **Moderador:**
        - Gerenciar usuários e artistas.
        - Cadastrar e geremcoar categorias

    - **Admin:**
        - Cadastrar moderadores.

## Tecnologias Utilizadas

- **ASP.NET Core**: Framework de desenvolvimento para construção da API.
- **Entity Framework Core**: ORM (Object-Relational Mapping) utilizado para facilitar a interação entre o sistema e o banco de dados MySQL.
- **MySQL**: Banco de dados relacional utilizado para armazenar os dados do sistema de maneira estruturada e eficiente.
- **Fluent Mapping**: Utilizado para configurar as entidades no código, permitindo personalizar o mapeamento de objetos para tabelas de banco de dados de maneira mais flexível.
- **Data Annotations**:  Configurar o comportamento das propriedades da entidade, como validações e restrições de dados.  
- **JWT (JSON Web Token)**: Utilizado para autenticação e autorização, garantindo a segurança das interações entre o cliente e a API.
