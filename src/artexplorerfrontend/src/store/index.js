import { createStore } from 'vuex';
import { vuexOidcCreateStoreModule } from 'vuex-oidc';
import { oidcSettings } from '../config/oidc';

export * from './mapping';

const mod = vuexOidcCreateStoreModule(
  oidcSettings,
  {
    namespaced: true,
    dispatchEventsOnWindow: true,
  },
  {
    userLoaded: (user) => console.log("OIDC user is loaded:", user),
    userUnloaded: () => console.log("OIDC user is unloaded"),
    accessTokenExpiring: () => console.log("Access token will expire"),
    accessTokenExpired: () => console.log("Access token did expire"),
    silentRenewError: () => console.log("OIDC user is unloaded"),
    userSignedOut: () => console.log("OIDC user is signed out"),
    oidcError: (payload) => console.log("OIDC error", payload),
    automaticSilentRenewError: (payload) =>
      console.log("OIDC automaticSilentRenewError", payload),
  }
);
console.log(
  mod
);

export default createStore({
  state: {},
  mutations: {},
  actions: {},
  modules: {
    oidcStore: mod,
  },
});
