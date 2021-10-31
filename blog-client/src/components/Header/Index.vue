<template>
  <b-navbar
    toggleable="lg"
    type="dark"
    variant="dark"
    class="justify-content-between"
  >
    <b-navbar-brand href="#">My BLOG</b-navbar-brand>
    <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>
    <b-collapse id="nav-collapse" is-nav>
      <b-navbar-nav>
        <b-nav-item href="/local" :active="isActive('Local')">Local</b-nav-item>
        <b-nav-item href="/remote" :active="isActive('Remote')"
          >Remote</b-nav-item
        >
        <b-nav-item href="/remote_plus" :active="isActive('RemotePlus')"
          >Remote +</b-nav-item
        >
      </b-navbar-nav>
    </b-collapse>
  </b-navbar>
</template>
<script>
export default {
  methods: {
    isActive(name) {
      return this.$route.name === name;
    },
  },
};
</script>
