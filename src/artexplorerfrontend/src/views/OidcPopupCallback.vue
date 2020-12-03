<template>
  <div>
  </div>
</template>

<script>
import { useStore } from 'vuex';
import { mapActions } from '../store';

export default {
  name: 'OidcPopupCallback',
  setup() {
    const store = useStore();
    const {oidcSignInPopupCallback} = mapActions(store, 'oidcStore', [
      'oidcSignInPopupCallback'
    ]);
    oidcSignInPopupCallback()
      .catch((err) => {
        console.error(err) // Handle errors any way you want
      })
    return {};
  },
}
</script>
