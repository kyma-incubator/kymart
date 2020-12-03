<template>
  <div>
  </div>
</template>

<script>
import { useStore } from 'vuex';
import jsonMarkup from 'json-markup';
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
        console.log('Oidc callback push redirect path', redirectPath)
        router.push(redirectPath)
      })
      .catch((err) => {
        console.log('Oidc callback error')
        console.error(err)
        router.push('/signin-oidc-error') // Handle errors any way you want
      });

    return {};
  },
}
</script>
