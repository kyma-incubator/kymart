<template>
  <div id="app">
    <div v-if="hasAccess" id="nav">
      <router-link to="/image">Home</router-link> |
      <router-link to="/protected">Protected</router-link> |
      <router-link to="/favourites" v-if="oidcIsAuthenticated"
        >Favourites</router-link
      >
      |
      <a v-if="oidcIsAuthenticated" href @click.prevent="signOut">Sign out</a>
      <a v-else href @click.prevent="authenticateOidcPopup">Sign in</a>
    </div>
    <router-view />
  </div>
</template>

<script>
import { onMounted, onUnmounted, computed } from "vue";
import { useStore } from "vuex";
import { useRouter, useRoute } from "vue-router";
import { mapActions } from "./store";

export default {
  name: "App",
  setup() {
    const store = useStore();
    const router = useRouter();
    const route = useRoute();

    const { authenticateOidcPopup, removeOidcUser } = mapActions(
      store,
      "oidcStore",
      ["authenticateOidcPopup", "removeOidcUser"]
    );

    const oidcIsAuthenticated = computed(
      () => store.getters["oidcStore/oidcIsAuthenticated"]
    );

    const hasAccess = computed(() => {
      return oidcIsAuthenticated.value || route.meta.isPublic;
    });

    const userLoaded = e => {
      console.log(
        "I am listening to the user loaded event in vuex-oidc",
        e.detail
      );
    };

    const oidcError = e => {
      console.log(
        "I am listening to the oidc oidcError event in vuex-oidc",
        e.detail
      );
    };

    const automaticSilentRenewError = e => {
      console.log(
        "I am listening to the automaticSilentRenewError event in vuex-oidc",
        e.detail
      );
    };

    const signOut = () => {
      removeOidcUser().then(() => router.push("/"));
    };

    onMounted(() => {
      window.addEventListener("vuexoidc:userLoaded", userLoaded);
      window.addEventListener("vuexoidc:oidcError", oidcError);
      window.addEventListener(
        "vuexoidc:automaticSilentRenewError",
        automaticSilentRenewError
      );
    });

    onUnmounted(() => {
      window.removeEventListener("vuexoidc:userLoaded", userLoaded);
      window.removeEventListener("vuexoidc:oidcError", oidcError);
      window.removeEventListener(
        "vuexoidc:automaticSilentRenewError",
        automaticSilentRenewError
      );
    });

    return {
      oidcIsAuthenticated,
      hasAccess,

      authenticateOidcPopup,
      removeOidcUser,

      userLoaded,
      oidcError,
      automaticSilentRenewError,
      signOut,

      router
    };
  }
};
</script>

<style>
#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
}
</style>
