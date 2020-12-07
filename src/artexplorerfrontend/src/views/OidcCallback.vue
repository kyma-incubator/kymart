<template>
  <div>
  </div>
</template>

<script>
import { useStore } from 'vuex';
import { mapActions } from '../store';
import { useRouter } from 'vue-router'

export default {
  name: 'OidcCallback',
  setup() {
    const router = useRouter()
    const store = useStore();
    const {oidcSignInCallback} = mapActions(store, 'oidcStore', ['oidcSignInCallback']);
    oidcSignInCallback()
      .then((redirectPath) => {
        router.push(redirectPath)
      })
      .catch((err) => {
        console.error(err)
        router.push('/signin-oidc-error') // Handle errors any way you want
      });

    return {};
  },
}
</script>
