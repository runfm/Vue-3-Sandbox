<template>
  <div id="ssp-page-wrapper">
    <header>
      <SspButton icon="menu" @click="menuClick" />
      <nav>
        <router-link to="/">Button</router-link> |
        <router-link to="/about">About</router-link>
      </nav>
    </header>
    <div id="ssp-main" class="ssp-main">
      <aside v-if="store.IsAsideVisible">
        <SspButton icon="widgets"  />
        <SspButton icon="lan"  />
        <SspButton icon="spa"  />
      </aside>
      <div class="ssp-main-content">
        <router-view />
      </div>
    </div>
    <footer></footer>
  </div>

</template>

<script lang="ts" setup>
import SspButton from "@/components/Generics/Button/index.vue"
import { useStore } from '@/store/pinia/index'

const store = useStore()

function menuClick() {
  store.ToogleAside()
}

</script>

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

#ssp-main {
  display: flex;
  gap: 0.5rem;
}

.ssp-main-content {
  flex-grow: 1;
}

header {
  display: flex;
}

aside {
  border-right: $--border;
  padding: 0px 8px;
  @include flex-v
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
