<template>
  <div
    v-if="oidcIsAuthenticated"
  >
    <p>
      You are signed in as:
    </p>
    <div style="width:100%;max-width:640px;height: 200px;margin: 0 auto;font-family: monospace;" v-html="userDisplay"></div>
    <p>
      Id token
    </p>
    <p>
      expires {{ new Date(oidcIdTokenExp).toISOString() }}
    </p>
    <textarea readonly style="width:100%;max-width:640px;height: 200px;margin: 0 auto;font-family: monospace;" v-model="oidcIdToken"></textarea>

    <p>
      <button @click="reauthenticate">Reauthenticate silently</button>
    </p>

  </div>
  <p v-else-if="oidcAuthenticationIsChecked">You are not signed in</p>
  <p v-else>Silent renew is in progress...</p>
</template>

<script>
import { useStore } from 'vuex';
import jsonMarkup from 'json-markup';
import { mapGetters, mapActions } from '../store';

export default {
  name: 'SignedInUser',
  setup() {
    const store = useStore();
    const computed = mapGetters(store, 'oidcStore', [
        'oidcIsAuthenticated',
        'oidcAuthenticationIsChecked',
        'oidcUser',
        'oidcIdToken',
        'oidcIdTokenExp'
      ]);
    
    const userDisplay = jsonMarkup(computed.oidcUser);

    const {authenticateOidcSilent, removeOidcUser} = mapActions(store, 'oidcStore', [
      'authenticateOidcSilent',
      'removeOidcUser'
    ]);

    function reauthenticate () {
      authenticateOidcSilent()
        .catch(() => this.removeOidcUser())
    }
    
    return {...computed, userDisplay, authenticateOidcSilent, removeOidcUser, reauthenticate};
  },
}
</script>

<style>
.json-markup {
  color: transparent;
}
.json-markup span {
  color: black;
  float: left;
}
.json-markup .json-markup-key {
  clear: left;
}
</style>
