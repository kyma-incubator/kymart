<template>
  <div>
    <p>Likes: {{ likesCount }}</p>
    <button v-if="oidcIsAuthenticated" @click="switchLike()">
      {{ myLike ? "Unlike" : "Like" }}
    </button>
  </div>
</template>

<script>
import { ref } from "vue";
import { useStore } from "vuex";
import { mapGetters } from "../store";

export default {
  name: "Likes",
  props: {
    imgId: String
  },
  setup(props) {
    const likesCount = ref(0);
    const myLike = ref(null);

    const store = useStore();
    const { oidcUser, oidcIdToken, oidcIsAuthenticated } = mapGetters(store, "oidcStore", [
      "oidcUser",
      "oidcIdToken",
      "oidcIsAuthenticated",
    ]);
  
    function fetchLikes() {
      // const headers = {Authorization: `Bearer ${oidcIdToken}`};
      return fetch(`/likes?imageId=${props.imgId}`)
        .then(result => result.json())
        .then(result => {
          // todo remove filtering after introducing
          result = result.filter(i => !!i.imageId);
          likesCount.value = result.length;
          myLike.value = result.find(e => e.email === oidcUser.email);
        });
    }

    async function Unlike() {
      try {
        await fetch(`/likes/${myLike.value.id}`, {
          method: "DELETE",
          headers: {
            Authorization: `Bearer ${oidcIdToken}`,
          }
        });
        myLike.value = null;
        likesCount.value--;
      } catch (e) {
        console.warn(e);
      }
    }

    async function Like() {
      const body = {
        imageId: props.imgId,
        version: "v1",
        email: oidcUser.email
      };
      try {
        const response = await fetch("/likes", {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify(body)
        });
        myLike.value = await response.json();
        likesCount.value++;
      } catch (e) {
        console.warn(e);
      }
    }

    async function switchLike() {
      if (myLike.value) {
        await Unlike();
      } else {
        await Like();
      }
    }

    fetchLikes();

    return { likesCount, myLike, oidcIsAuthenticated, switchLike };
  }
};
</script>

<style scoped>
img {
  max-width: 100%;
}
</style>
