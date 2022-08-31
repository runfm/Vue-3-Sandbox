<template>
  <div id="ssp-page-wrapper">
    <header>
      <nav>
        <router-link to="/">Button</router-link> |
        <router-link to="/about">About</router-link>
      </nav>
    </header>
    <div id="ssp-main" class="ssp-main">
      <aside></aside>
      <div class="ssp-main-content">
        <router-view />
      </div>
    </div>
    <footer></footer>
  </div>

</template>

<style lang="scss">
#ssp-page-wrapper {
  display: flex;
  flex-direction: column;
  height: 100vh;

  header,
  footer {
    padding: 0.5rem;
  }

  header {
    background-color: mix(white, #42b983, 80%);
  }

  footer {
    background-color: mix(black, #42b983, 50%);
  }

  #ssp-main {
    flex-grow: 1;
    padding: 0.5rem;
  }
}

body {
  margin: 0px !important;
}

html {
  font-size: $--font-size;
}

#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

nav {
  text-align: left;
  padding: 0.5rem;

  a {
    font-weight: bold;
    color: #2c3e50;

    &.router-link-exact-active {
      color: #42b983;
    }
  }
}
</style>
