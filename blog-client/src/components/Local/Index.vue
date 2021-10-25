<template>
  <b-container class="post-container">
    <Post />
  </b-container>
</template>
<script>
import Post from "@/components/Post/Index.vue";
export default {
  name: "Local",
  components: {
    Post,
  },
};
</script>
