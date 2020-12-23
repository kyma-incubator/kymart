<template>
  <div>
    <p>Likes: {{ likesCount }}</p>
    <button v-if="oidcIsAuthenticated" @click="switchLike()">
      {{ isLiked ? "Unlike" : "Like" }}
    </button>
  </div>
</template>

<script>
import { ref } from "vue";
import { useStore } from "vuex";
import { mapGetters } from "../store";

const server = '';

export default {
  name: "Likes",
  props: {
    imgId: String,
    imgVersion: String,
  },
  setup(props) {
    const likesCount = ref(0);
    const isLiked = ref(null);

    const store = useStore();
    const { oidcUser, oidcIdToken, oidcIsAuthenticated } = mapGetters(store, "oidcStore", [
      "oidcUser",
      "oidcIdToken",
      "oidcIsAuthenticated",
    ]);

    function fetchLikes() {
      let endpoint = `${server}/likes?imageId=${props.imgId}&imageVersion=${props.imgVersion}`;
      if (oidcUser) {
        endpoint += `&email=${oidcUser.email}`;
      }
      const headers = {Authorization: `Bearer ${oidcIdToken}`};
      return fetch(endpoint, { headers })
        .then(result => result.json())
        .then(result => {
          likesCount.value = result.likesCount;
          isLiked.value = result.isLiked;
        });
    }

    async function Unlike() {
      const body = {
        imageId: props.imgId,
        imageVersion: "v1",
        email: oidcUser.email,
      };
      try {
        await fetch(`${server}/likes`, {
          method: "DELETE",
          headers: {
            Authorization: `Bearer ${oidcIdToken}`,
            "Content-Type": "application/json",
          },
          body: JSON.stringify(body),
        });
        isLiked.value = false;
        likesCount.value--;
      } catch (e) {
        console.warn(e);
      }
    }

    async function Like() {
      const body = {
        imageId: props.imgId,
        imageVersion: "v1",
        email: oidcUser.email
      };
      try {
        const response = await fetch(`${server}/likes`, {
          method: "POST",
          headers: { "Content-Type": "application/json", Authorization: `Bearer ${oidcIdToken}`, },
          body: JSON.stringify(body),
        });
        isLiked.value = true;
        likesCount.value++;
      } catch (e) {
        console.warn(e);
      }
    }

    async function switchLike() {
      if (isLiked.value) {
        await Unlike();
      } else {
        await Like();
      }
    }

    fetchLikes();

    return { likesCount, isLiked, oidcIsAuthenticated, switchLike };
  }
};
</script>

<style scoped>
img {
  max-width: 100%;
}
</style>
