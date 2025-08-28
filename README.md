<h1>🎬 MyWatchList - Plataforma de Gerenciamento de Obras Audiovisuais</h1>

<h2>📋 Descrição do Projeto</h2>
<p>O projeto MyWatchList é uma plataforma web desenvolvida para que usuários possam explorar, avaliar e organizar seus filmes, séries e animes favoritos. Inspirado em sistemas como IMDb e MyAnimeList, o objetivo principal é permitir o gerenciamento da experiência do usuário com obras audiovisuais de forma personalizada e interativa.</p>

<h2>🎯 Objetivo</h2>
<p>Desenvolver uma plataforma completa com funcionalidades de busca, avaliação, comentários e organização de obras audiovisuais, proporcionando uma experiência similar ao IMDb com recursos personalizados para os usuários.</p>

<h2>🛠️ Tecnologias Utilizadas</h2>

<h3>Backend</h3>
<ul>
  <li><b>C#</b> - Linguagem de programação principal</li>
  <li><b>ASP.NET Core</b> - Framework web</li>
  <li><b>Razor Pages</b> - Modelo de aplicação web</li>
  <li><b>Entity Framework Core</b> - ORM para acesso a dados</li>
  <li><b>SQL Server</b> - Banco de dados relacional</li>
</ul>

<h3>Frontend</h3>
<ul>
  <li><b>HTML5</b> - Estrutura das páginas</li>
  <li><b>CSS3</b> - Estilização e design responsivo</li>
  <li><b>Razor Syntax</b> - Integração backend-frontend</li>
  <li><b>JavaScript</b> - Interatividade e funcionalidades client-side</li>
</ul>

<h2>📊 Funcionalidades Implementadas</h2>

<h3>🎬 Página Individual de Obras</h3>
<ul>
  <li>Exibição de sinopse da obra</li>
  <li>Nota média baseada nas avaliações dos usuários</li>
  <li>Galeria de fotos e vídeos relacionados</li>
  <li>Listagem do elenco principal</li>
  <li>Para séries e animes: lista de episódios por temporada</li>
</ul>

<h3>⭐ Sistema de Avaliação e Interação</h3>
<ul>
  <li>Marcar obras como assistidas</li>
  <li>Adicionar obras à watchlist</li>
  <li>Avaliar obras com nota de 0 a 10 estrelas</li>
  <li>Sistema de comentários com status e progresso</li>
  <li>Recomendações de obras similares</li>
</ul>

<h3>🏠 Tela Inicial</h3>
<ul>
  <li>Obras em alta (dia, semana, mês)</li>
  <li>Obras mais bem avaliadas</li>
  <li>Obras mais populares</li>
  <li>Filtragem por gênero</li>
  <li>Celebridades em alta</li>
</ul>

<h3>🔍 Sistema de Busca e Filtros</h3>
<ul>
  <li>Busca por nome de filmes, séries e animes</li>
  <li>Busca por celebridades</li>
  <li>Filtros por tipo (filme, série, anime)</li>
</ul>

<h3>👤 Gestão de Perfil</h3>
<ul>
  <li>Visualização da watchlist pessoal</li>
  <li>Criação de listas personalizadas</li>
  <li>Edição de perfil (nome, foto, biografia)</li>
</ul>

<h3>🔐 Sistema de Autenticação</h3>
<ul>
  <li>Criação de conta (sign up)</li>
  <li>Login de usuários (sign in)</li>
  <li>Logout da conta</li>
</ul>

<h2>🚀 Como Executar</h2>

<p><b>Pré-requisitos</b></p>
<ul>
  <li>.NET 6.0 SDK ou superior</li>
  <li>SQL Server (se preferir pode usar o SQLlite, basta configurar no appsettings.json e no Program.cs)</li>
  <li>Visual Studio 2022 ou VS Code</li>
</ul>

<p><b>Clone o repositório:</b></p>
<pre>git clone https://github.com/RomarioSilva98/MyWatchList.git</pre>

<p><b>Execução do Projeto</b></p>
<pre>dotnet restore
dotnet ef database update
dotnet run</pre>

<h2>🎨 Estrutura do Projeto</h2>

<h3>📁 Organização de Arquivos</h3>
<ul>
  <li><b>Pages/</b> - Páginas Razor (.cshtml e .cshtml.cs)</li>
  <li><b>Models/</b> - Modelos de dados e entidades</li>
  <li><b>Data/</b> - Contexto do banco e configurações</li>
  <li><b>wwwroot/css/</b> - Arquivos de estilização CSS</li>
</ul>

<h3>🗃️ Principais Entidades</h3>
<ul>
  <li><b>Usuario</b> - Gerencia informações do usuário</li>
  <li><b>Obra</b> - Entidade base para filmes, séries e animes</li>
  <li><b>Filme/Serie/Anime</b> - Especializações de Obra</li>
  <li><b>Comentario</b> - Avaliações e comentários dos usuários</li>
  <li><b>Ator</b> - Informações sobre celebridades</li>
  <li><b>WatchList</b> - Lista de Obras que o Usuário Pretende Assistir</li>
</ul>

<h2>🎯 Funcionalidades Técnicas</h2>



<h3>🎨 Design Responsivo</h3>
<ul>
  <li>Layout adaptável para mobile e desktop</li>
  <li>Paleta de cores inspirada no tema "dark mode"</li>
  <li>Animações e transições suaves</li>
</ul>

<h3>🔒 Segurança</h3>
<ul>
  <li>Autenticação e autorização de usuários</li>
  <li>Validação de dados de entrada</li>
</ul>

<h2>👥 Desenvolvido por</h2>
<p>Romário</p>
<p>Sonayte</p>
<p>Izabel</p>

<h2>📄 Licença</h2>
<p>Este projeto foi desenvolvido para fins acadêmicos como parte da avaliação da disciplina Web II.</p>
